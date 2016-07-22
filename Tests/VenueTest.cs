using Xunit;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnTrueIfNamesAreTheSame()
    {
      //Arrange
      Venue firstVenue = new Venue("Cool Haps");
      Venue otherFirstVenue = new Venue("Cool Haps");

      //Act Assert
      Assert.Equal(firstVenue, otherFirstVenue);
    }


    public void Dispose()
    {

    }
  }
}
