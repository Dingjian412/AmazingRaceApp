using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazingRaceApp.Models
{
    public class EventTeamModel
    {
        public int TeamId { get; set; }
        public string TeamName{ get; set; }
        public Nullable<int> NumOfClearedStop { get; set; }
        public Nullable<decimal> DistanceToNextStop { get; set; }
        public Nullable<decimal> CurrentLat { get; set; }
        public Nullable<decimal> CurrentLng { get; set; }
        public Nullable<int> Ranking { get; set; }

    }

    public class EventStaffModel
    {
        public int StaffId { get; set; }
        public Nullable<decimal> CurrentLat { get; set; }
        public Nullable<decimal> CurrentLng { get; set; }
    }

    public class LiveEventModel
    {
        public List<EventTeamModel> InvolvedTeams { get; set; }
        public List<EventStaffModel> InvolvedStaff { get; set; }
    }

}