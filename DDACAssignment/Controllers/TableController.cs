﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();
            CloudStorageAccount storagetable = CloudStorageAccount.Parse(configure["ConnectionStrings:DDACTableStorageConnection"]);

            CloudTableClient tableclient = storagetable.CreateCloudTableClient();
            CloudTable table = tableclient.GetTableReference("DispatcherTable");
            return table;
        }

        public ActionResult CreateTable()
        {
            CloudTable table = getTableStorageInformation();
            ViewBag.Success = table.CreateIfNotExistsAsync().Result;
            ViewBag.TableName = table.Name;
            return View();
        }
    }

}
