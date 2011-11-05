﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kolejki.F
{
    public abstract class Queue : IQueue
    {
        public String Name { get; set; }
        public List<Job> JobList {get; set;}

        public Scheduler scheduler;
        
        public Queue(Scheduler s, int size)
        {
            scheduler = s;
            Size = size;
            JobList = new List<Job>();
        }

        public override string ToString()
        {
            String s = Name + ": ";

            foreach (var x in JobList)
            {
                s += x.Id + ", ";
            }

            return s;
        }

        public abstract Job Get();

        public abstract Job Peak();

        public abstract bool Put(Job job);

        public int Size { get; set; }

        public int Count { get { return JobList.Count; } }

        public bool IsFull { get { if (Count >= Size) return true; return false; } }

        public bool IsEmpty { get { if (JobList.Count == 0) return true; return false; } }

        public void AddEventPut()
        {
            PutToQueueEvent ev = new PutToQueueEvent(scheduler.timestamp, this);
            scheduler.AddEvent(ev);
        }

        public void AddEventGet()
        {
            GetFromQueueEvent ev = new GetFromQueueEvent(scheduler.timestamp, this);
            scheduler.AddEvent(ev);
        }
       
    }
}