﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Survey.Models
{
    public class AnswerModel
    {
        public string Code { get; set; }
        public string NameSurname { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}