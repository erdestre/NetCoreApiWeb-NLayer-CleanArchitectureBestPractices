﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
	public class BaseEntity<T> 
	{
		public T Id { get; set; } = default!;
	}
}
