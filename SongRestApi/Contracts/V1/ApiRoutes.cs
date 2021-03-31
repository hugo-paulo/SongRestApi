using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongRestApi.Contracts.V1
{
    public static class ApiRoutes
    {
        /** 
         * This will be a central class that will hold the name of the routes 
         * instead of adding them to the attributes in the controller class.
         * This to help control controllers with versioning 
         * **/

        public const string Root = "api";
        public const string Version = "v1";
        public const string UrlPrefix = Root + "/" + Version;

        //This will represent the routes for the album controller
        public class album
        {
            //note cant use string interpolation with const because it uses a method
            public const string GetAllAlbums = UrlPrefix + "/album";
            public const string GetSingleAlbum = UrlPrefix + "/album/{id}";
            public const string CreateAlbum = UrlPrefix + "/album";
            //Two versions of update one with PUT and PATCH
            public const string UpdateAlbum = UrlPrefix + "/album/{id}";
            public const string PatchAlbum = UrlPrefix + "/album/{id}";
            public const string DeleteAlbum = UrlPrefix + "/album/{id}";
        }
        
    }
}
