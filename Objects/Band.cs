using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _bandName;

    public Band(string bandName, int id = 0)
    {
      _bandName = bandName;
      _id = id;
    }

    
  }
}
