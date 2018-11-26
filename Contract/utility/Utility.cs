using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sanita.Utility.Logger;

namespace Contract.utility
{
    public static class Utility
    {
        //Folder smart contract
        private const String SMARTCONTRACT = "SmartContracts";

        //Link to project
        private static string _root;

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
        public static string mRoot
        {
            get
            {
                _root = _root ?? Directory.GetCurrentDirectory();
                return _root;
            }
        }

        //Get direction to file contract
        public static string GetPathFileContract(string fileName)
        {
            string pathFile = Path.Combine(mRoot, SMARTCONTRACT, fileName);
            SanitaLog.Log("Path file", pathFile);
            return pathFile;
        }

        public static string CombineUri(params string[] paths)
        {
            Uri baseUri = null;
            Uri myUri = null;
            foreach (string path in paths)
            {
                baseUri = baseUri ?? new Uri(path);
                if (baseUri == null)
                {
                    baseUri = new Uri(path);
                }
                else
                {
                    myUri = new Uri(baseUri, path);
                }
            }
            return myUri.ToString();
        }

        public static IEnumerable<int> FilterArray()
        {
            // Specify the data source.
            int[] scores = new int[] { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            return scoreQuery;
        }

        public static string ToStringList(List<string> mList)
        {
            return ToStringList(mList.ToArray());
        }

        public static string ToStringList(string[] mList)
        {
            return string.Join(",", mList);
        }
    }
}