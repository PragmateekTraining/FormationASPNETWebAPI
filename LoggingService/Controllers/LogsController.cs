﻿using Logging.Model;
using Logging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Logging.Service.Controllers
{
    public class LogsController : ApiController
    {
        private readonly string connectionString = null;

        public LogsController()
            : this(null)
        {
        }

        public LogsController(string connectionString)
        {
            this.connectionString = connectionString ?? "prodConnection";
        }

        public Log[] GetAll()
        {
            using (LogsRepository repository = new LogsRepository(connectionString))
            {
                return repository.GetAllLogs();
            }
        }

        public void Add(Level level, string message)
        {
            using (LogsRepository repository = new LogsRepository(connectionString))
            {
                repository.Add(level, message);
            }
        }
    }
}
