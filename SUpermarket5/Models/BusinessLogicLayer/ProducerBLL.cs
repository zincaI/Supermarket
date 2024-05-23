﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUpermarket5.Models.BusinessLogicLayer
{
    public class ProducerBLL
    {
        public bool IsActive { get; set; } = true;
        private Supermarket4Entities context = new Supermarket4Entities();
        public ObservableCollection<Producer> ProducerList { get; set; }
        public string ErrorMessage { get; set; }

        public ProducerBLL()
        {
            ProducerList = new ObservableCollection<Producer>();
        }
        public ObservableCollection<Producer> GetAllProducer()
        {
            List<Producer> producers = context.Producers.Where(c => c.IsActive).ToList();
            ObservableCollection<Producer> prod = new ObservableCollection<Producer>();
            foreach (Producer producer in producers)
            {
                prod.Add(producer);
            }
            return prod;
        }
        public void AddMethod(object obj)
        {
            Producer producer = obj as Producer;
            if (producer != null)
            {
                bool ok = false;
                foreach (Producer prod in ProducerList)
                    if (prod.ProducerID == producer.ProducerID)
                        ok = true;
                if (ok)
                {
                    context.RestoreCategory(producer.ProducerID);
                    context.ModifyCategory(producer.ProducerID, producer.Name);
                }
                else
                {
                    if (string.IsNullOrEmpty(producer.Name))
                    {
                        ErrorMessage = "Numele producatorului nu este precizat";
                    }
                    else
                    {
                        context.Producers.Add(producer);
                        context.SaveChanges();
                        producer.ProducerID = context.Producers.Max(item => item.ProducerID);
                        ProducerList.Add(producer);
                        ErrorMessage = "";
                    }
                }
            }
        }
        public void UpdateMethod(object obj)
        {
            Producer prod = obj as Producer;
            if (prod == null)
            {
                ErrorMessage = "Selecteaza producator";
            }
            else if (string.IsNullOrEmpty(prod.Name))
            {
                ErrorMessage = "Numele producatorului trebuie precizat";
            }
            else
            {
                bool ok = false;
                foreach (Producer p in ProducerList)
                    if (p.ProducerID == prod.ProducerID && p.IsActive == true)
                        ok = true;
                if (ok)
                {
                    context.ModifyProducer(prod.ProducerID, prod.Name, prod.Country);
                    context.SaveChanges();
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Nu exista producator.";
                }
            }
        }
        public void DeleteMethod(object obj)
        {
            Producer prod = obj as Producer;
            if (prod == null)
            {
                ErrorMessage = "Selecteaza o producator";
            }
            else
            {
                Producer p = context.Producers.Where(i => i.ProducerID == prod.ProducerID).FirstOrDefault();
                if (p != null)
                {
                    context.DeleteCategory(p.ProducerID);
                    context.SaveChanges();
                    ProducerList.Remove(p);
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Producatorul nu a fost găsit";
                }
            }
        }
    }
}