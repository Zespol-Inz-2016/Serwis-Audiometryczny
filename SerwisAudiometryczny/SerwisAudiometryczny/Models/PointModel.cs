using SerwisAudiometryczny.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SerwisAudiometryczny.Models
{
    public class PointModel : MyBaseModel, IMyBase
    {
        public PointModel() : base()
        {
            Type = "PointModel";
        }
        public int AudiogramID { get; set; }
        public int Frequency { get; set; }
        public float HearingThresholdLevel { get; set; }

    }
}