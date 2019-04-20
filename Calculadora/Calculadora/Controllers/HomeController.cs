using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Ecra = "0";
            Session["numero"] = "0";
            Session["primeiroOperador"] = true;
            Session["guarda"] = "";
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor){
            string ecra = visor;
            switch (bt){
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (visor=="0") {
                        ecra = bt;
                        Session["numero"] = bt;
                    }
                    else{
                        ecra += bt;
                        Session["numero"] += bt;
                    }
                    break;
                case ",":
                    if(!((string)Session["numero"]).Contains(",")){
                        ecra += bt;
                        Session["numero"] += bt;
                    }
                    break;
                case "+":
                case "-":
                case "x":
                case ":":
                    if((bool)Session["primeiroOperador"] == true){
                        ecra += bt;
                        Session["guarda"] = (string)Session["numero"] + bt;
                        Session["valor1"] = Session["numero"];
                        Session["primeiroOperador"] = false;
                    }
                    else{
                        Session["valor2"] = Session["numero"];
                        Session["valor1"] = double.Parse((string)Session["valor1"]);
                        Session["valor2"] = double.Parse((string)Session["valor2"]);
                        Session["valor1"] = valor((double)Session["valor1"], (double)Session["valor2"], (string)Session["operador"]);
                        ecra = Session["valor1"] + bt;
                        Session["guarda"] = Session["valor1"] + bt;
                    }
                    Session["numero"] = "";
                    Session["operador"] = bt;
                    break;
                case "C":
                    ecra = "0";
                    Session["numero"] = "0";
                    Session["primeiroOperador"] = true;
                    Session["guarda"] = "";
                    Session["operador"] = "";
                    Session["valor1"] = "";
                    Session["valor2"] = "";
                    break;
                case "+/-":
                    if((string)Session["numero"] != ""){
                        Session["numero"] = double.Parse((string)Session["numero"]) * -1;
                        Session["numero"] = Session["numero"].ToString();
                    }
                    ecra = (string)Session["guarda"] + (string)Session["numero"];
                    break;
                case "=":
                    Session["valor2"] = Session["numero"];
                    Session["valor1"] = double.Parse((string)Session["valor1"]);
                    Session["valor2"] = double.Parse((string)Session["valor2"]);
                    Session["valor1"] = valor((double)Session["valor1"], (double)Session["valor2"], (string)Session["operador"]);
                    ecra = (string)Session["valor1"];
                    Session["guarda"] = "";
                    Session["numero"] = Session["valor1"];
                    Session["primeiroOperador"] = true;
                    break;

            }
            ViewBag.Ecra = ecra;
            return View();
        }

        public String valor(double valor1, double valor2, string bt){
            switch (bt){
                case "+":
                    valor1 += valor2;
                    break;
                case "-":
                    valor1 -= valor2;
                    break;
                case "x":
                    valor1 *= valor2;
                    break;
                case ":":
                    valor1 /= valor2;
                    break;
            }
            return valor1.ToString();
        }
    }
}