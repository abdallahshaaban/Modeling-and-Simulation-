﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChannelQueueModels
{
    public class Server
    {
        public Server()
        {
            ServiceTimeDistribution = new List<TimeDistribution>();
        }
        public Server(int serverId , string name , double serviceEfficiency,List<TimeDistribution>serviceTimeDistribution)
        {
            ServerId = serverId;
            Name = name;
            ServiceEfficiency = serviceEfficiency;
            ServiceTimeDistribution = new List<TimeDistribution>();
            ServiceTimeDistribution = serviceTimeDistribution;
        }
        public int ServerId { get; set; }

        public string Name { get; set; }

        public double ServiceEfficiency { get; set; }

        public List<TimeDistribution> ServiceTimeDistribution { get; set; }
    }
}
