﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Model
{
    public class NewsDTO
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}