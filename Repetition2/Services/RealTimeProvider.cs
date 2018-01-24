using Repetition2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repetition2.Services
{
    public class RealTimeProvider : ITimeProvider
    {
		public DateTime Now { get => DateTime.Now; set => throw new NotImplementedException(); }
	}
}
