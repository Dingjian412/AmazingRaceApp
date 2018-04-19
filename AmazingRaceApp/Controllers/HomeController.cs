using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazingRaceApp.Models;
using AmazingRaceApp.DAL;
using AutoMapper;

namespace AmazingRaceApp.Controllers
{
    public class HomeController : Controller
    {
        //public List<EventTeamModel> teams = new List<EventTeamModel>();

        public JsonResult LoadRankingList()
        {
            var rank = LoadEventTeams(1);
            return Json(new { data = rank }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeamLocation()
        {
            var data = LoadEventTeams(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPitStopLocation()
        {
            var data = LoadEventPitStop(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStaffLocation()
        {
            var data = LoadEventStaff(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public List<EventTeamModel> LoadEventTeams(int eventId)
        {
            List<EventTeamModel> eventTeams = new List<EventTeamModel>();

            using (AECEventManagementEntities context = new AECEventManagementEntities())
            {
                eventTeams = (from et in context.EventTeams
                              join t in context.Teams on et.TeamID equals t.TeamID
                              where et.EventID == eventId
                              select new EventTeamModel
                              {
                                  TeamId = t.TeamID,
                                  TeamName = t.TeamName,
                                  CurrentLat = et.CurrentLatitude,
                                  CurrentLng = et.CurrentLongtitude,
                                  DistanceToNextStop = et.DistanceToNext,
                                  NumOfClearedStop = et.NumOfClearned
                              }).OrderByDescending(x=>x.NumOfClearedStop).ThenBy(x=>x.DistanceToNextStop).ToList();
            }

            return eventTeams;
        }

        public List<EventStaffModel> LoadEventStaff(int eventId)
        {
            List<EventStaffModel> eventStaff = new List<EventStaffModel>();

            using (AECEventManagementEntities context = new AECEventManagementEntities())
            {
                
            }

            return eventStaff;
        }

        public List<EventPitStopModel> LoadEventPitStop(int eventId)
        {
            List<EventPitStopModel> eventPit = new List<EventPitStopModel>();

            using (AECEventManagementEntities context = new AECEventManagementEntities())
            {
                eventPit = (from ep in context.EventPits
                            join p in context.PitStops on ep.PitStopID equals p.PitStopID
                            where ep.EventID == eventId
                            select new EventPitStopModel
                            {
                                PitStopId = ep.PitStopID,
                                PitStopName = p.PitName,
                                CurrentLat = ep.Lat,
                                CurrentLng = ep.Lng
                            }).ToList();
            }

            return eventPit;
        }

        public JsonResult MessageProcessor(int eventId, int teamId, decimal latitude, decimal longtitude, int stopId)
        {
            bool result = true;
            //using (AECEventManagementEntities context = new AECEventManagementEntities()) {
            //    EventTeam obj = (from o in context.EventTeams where o.EventID == eventId && o.TeamID == teamId select o).FirstOrDefault();
            //    obj.CurrentLatitude = Convert.ToDecimal(latitude);
            //    obj.CurrentLongtitude = Convert.ToDecimal(longtitude);
            //    obj.NumOfClearned = stopId;
            //    context.SaveChanges();
            //    result = true;
            //}
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public List<EventTeamModel> LoadEventTeams(int eventId)
        //{
        //    List<EventTeamModel> eventTeams = new List<EventTeamModel>();
        //    List<InvolvedTeam> teams = new List<InvolvedTeam>();
        //    using (AECEventManagementEntities context = new AECEventManagementEntities())
        //    {
        //        teams = (from t in context.InvolvedTeams where t.EventID == eventId select t).ToList();
        //    }

        //    eventTeams= UpdateEventTeams(teams.OrderByDescending(t=>t.NumOfClearned).ThenBy(t=>t.DistanceToNext).ToList());
        //    return eventTeams;
        //}

        //public List<EventTeamModel> UpdateEventTeams(List<InvolvedTeam> inTeam)
        //{
        //    List<EventTeamModel> result = new List<EventTeamModel>();
        //    int i = 0;
        //    foreach (var team in inTeam)
        //    {
        //        i++;
        //        EventTeamModel model = new EventTeamModel();
        //        model.TeamId = team.TeamID;
        //        model.TeamName = team.TeamName;
        //        model.CurrentLat = team.CurrentLatitude;
        //        model.CurrentLng = team.CurrentLongtitude;
        //        model.NumOfClearedStop = team.NumOfClearned;
        //        model.DistanceToNextStop = team.DistanceToNext;
        //        model.Ranking = i;
        //        result.Add(model);
        //    }

        //    return result;
        //}

    }
}