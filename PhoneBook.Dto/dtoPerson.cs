﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Dto
{
  public class dtoPerson
  {
    public Guid Id { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Company { get; set; }
    public string ContactInfoId { get; set; }
  }


}