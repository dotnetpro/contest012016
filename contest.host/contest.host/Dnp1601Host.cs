using System;
using System.IO;
using contestrunner.contract.host;
using contest.submission.contract;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace contest.host
{

  public class Dnp1601Host : IHost
  {
    string stopmessage = "";
    
    public void Prüfen(object beitrag, string wettbewerbspfad, string beitragsverzeichnis)
    {
      var sut = (IDnp1601Solution)beitrag;
      var steps = new List<Point>();

      DateTime starttime = DateTime.Now;


//      Status(new Prüfungsstatus() { Statusmeldung = "Start: " + DateTime.Now.ToLongTimeString() }); 
      sut.Result += x => Console.WriteLine(x);
      sut.Start("Text über die liebe Verwandtschaft.");
      
      var anfang = new Prüfungsanfang { Wettbewerb = Path.GetFileName(wettbewerbspfad), Beitrag = Path.GetFileName(beitragsverzeichnis) };
      Anfang(anfang);
      sut.Process("Karl", "Benedikt");
      
      Status(new Prüfungsstatus() { Statusmeldung = stopmessage });

      Ende(new Prüfungsende(){ Dauer = DateTime.Now.Subtract(starttime) });

    }

    public event Action<Prüfungsanfang> Anfang;
    public event Action<Prüfungsstatus> Status;
    public event Action<Prüfungsende> Ende;
    public event Action<Prüfungsfehler> Fehler;
  }

}
