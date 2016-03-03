using System;
using System.Collections.Generic;
using System.Data.Entity;


namespace DAL
{
    class StuLanConfiguration:DbConfiguration
    {

        public StuLanConfiguration()
        {
            SetProviderServices("MySql.Data.MySqlClient", new MySql.Data.MySqlClient.MySqlProviderServices());
            SetDatabaseInitializer(new StulanInitializer());
        }
            
    }
}
