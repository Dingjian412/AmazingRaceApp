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

        public ActionResult Index()
        {
            //LiveEventModels liveevent = new LiveEventModels();

            //List<TeamModels> teams = new List<TeamModels>();
            //TeamModels team1 = new TeamModels();
            //team1.TeamId = 1;
            //team1.TeamName = "team1";
            //team1.NumOfClearedStop = 2;
            //teams.Add(team1);

            //TeamModels team2 = new TeamModels();
            //team2.TeamId = 2;
            //team2.TeamName = "team2";
            //team2.NumOfClearedStop = 3;
            //teams.Add(team2);

            //liveevent.Teams = teams;

            //return View(liveevent);
            return View();
        }

        public JsonResult LoadRankingList()
        {
            var rank = LoadEventTeams(1);
            return Json(new {data= rank }, JsonRequestBehavior.AllowGet);
        }

        public List<EventTeamModel> LoadEventTeams(int eventId)
        {
            List<EventTeamModel> eventTeams = new List<EventTeamModel>();
            List<InvolvedTeam> teams = new List<InvolvedTeam>();
            using (AECEventManagementEntities context = new AECEventManagementEntities())
            {
                teams = (from t in context.InvolvedTeams where t.EventID == eventId select t).ToList();
            }

            eventTeams= UpdateEventTeams(teams.OrderByDescending(t=>t.NumOfClearned).ThenBy(t=>t.DistanceToNext).ToList());
            return eventTeams;
        }

        public List<EventTeamModel> UpdateEventTeams(List<InvolvedTeam> inTeam)
        {
            List<EventTeamModel> result = new List<EventTeamModel>();
            int i = 0;
            foreach (var team in inTeam)
            {
                i++;
                EventTeamModel model = new EventTeamModel();
                model.TeamId = team.TeamID;
                model.TeamName = team.TeamName;
                model.CurrentLat = team.CurrentLatitude;
                model.CurrentLng = team.CurrentLongtitude;
                model.NumOfClearedStop = team.NumOfClearned;
                model.DistanceToNextStop = team.DistanceToNext;
                model.Ranking = i;
                result.Add(model);
            }

            return result;
        }

        public JsonResult MessageProcesser(int eventId, int teamId, decimal latitude, decimal longtitude, int stopId)
        {
            bool result = false;
            using (AECEventManagementEntities context = new AECEventManagementEntities()) {
                EventTeam obj = (from o in context.EventTeams where o.EventID == eventId && o.TeamID == teamId select o).FirstOrDefault();
                obj.CurrentLatitude = Convert.ToDecimal(latitude);
                obj.CurrentLongtitude = Convert.ToDecimal(longtitude);
                obj.NumOfClearned = stopId;
                context.SaveChanges();
                result = true;
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}