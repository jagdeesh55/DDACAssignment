using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDACAssignment.Models;

namespace DDACAssignment.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private CloudTable getTableStorageInformation()
        {
            //link to appsettings.json and read connection string
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();
            CloudStorageAccount storageaccount = CloudStorageAccount.Parse(configure["ConnectionStrings:DDACTableStorageConnection"]);

            //table creation
            CloudTableClient tableclient = storageaccount.CreateCloudTableClient();
            CloudTable table = tableclient.GetTableReference("DispatcherTable");

            return table;
        }

        public ActionResult CreateTable (string tablename = null,bool success = false)
        {
            ViewBag.TableName = tablename;
            ViewBag.Success = success;
            return View();
        }

        public ActionResult Create ()
        {
            string tablename;
            bool success;
            //link to storage account function getTableStorageInformation()
            CloudTable table = getTableStorageInformation();

            //creates table if doesnt exist
            success = Convert.ToBoolean(table.CreateIfNotExistsAsync().Result);

            //grab table name
            tablename = table.Name;
            return RedirectToAction("CreateTable", "Table",new { tablename = tablename,success = success });
        }

        //View All order page
        public ActionResult ReadyOrders(string insertion = null)
        {
            ViewBag.insert = insertion;
            return View();
        }

        public ActionResult Dispatch(string orderid, string dispatchingid)
        {
            ViewBag.Orderid = orderid;
            ViewBag.Dispatchid = dispatchingid;
            return View();
        }

        //click select go to adddispatchingpage
        public ActionResult SelectOrder()
        {
            string orderid,dispatchingid;
            //get orderid from SQL
            /////------
            orderid = "121";
            dispatchingid = "DP" + orderid;
            return RedirectToAction("Dispatch","Table", new { dispatchingid = dispatchingid, orderid = orderid });
        }

        //insert dispatching data into table
        public ActionResult AddDispatching(string dispatchingid, string orderid)
        {
            CloudTable tablestorage = getTableStorageInformation();

            DispatchingEntity dispatch = new DispatchingEntity(dispatchingid, orderid);
            dispatch.Status = "Not completed";
            string insertion = "";
            TableOperation insertOperation = TableOperation.Insert(dispatch);
            try
            {
                TableResult result = tablestorage.ExecuteAsync(insertOperation).Result;
                insertion = " Order Dispatched";
            }
            catch (AggregateException e)
            {
                insertion = " Order Already Dispatched" + e.ToString();
            }
            return RedirectToAction("ReadyOrders", "Table", new { insertion = insertion });
        }

        public ActionResult ViewDispatchOrders()
        {
            CloudTable tablestorage = getTableStorageInformation();
            string msg = null;
            try
            {
                TableQuery<DispatchingEntity> search = new TableQuery<DispatchingEntity>()
                    .Where(TableQuery.GenerateFilterCondition("Status", QueryComparisons.Equal,"Not Completed"));
                List<DispatchingEntity> result = new List<DispatchingEntity>();
                TableContinuationToken token = null;
                do
                {
                    TableQuerySegment<DispatchingEntity> data = tablestorage.ExecuteQuerySegmentedAsync(search, token).Result;
                    token = data.ContinuationToken;
                    foreach (DispatchingEntity dispatches in data.Results)
                    {
                        result.Add(dispatches);
                    }
                } while (token != null);
                if (result.Count != 0)
                {
                    return View(result);
                }
                else
                {
                    msg = "No Data Found";
                    return View();
                }

            }catch(Exception e)
            {
                msg = "Exception " + e.ToString(); 
            }
            return View();
        }
    }

}
