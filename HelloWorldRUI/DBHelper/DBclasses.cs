using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldRUI.DBHelper
{
    class requestedmovies
    {
        requestedmovies()
        {

        }

        public void Add_movie(movie movie_toAdd)
        {
            requested_movie_list.Add(movie_toAdd);
        }

        public List<movie> Get_movie_list()
        {
            return requested_movie_list;
        }

        public void Clear()
        {
            if (requested_movie_list.Count > 0)
            {
                requested_movie_list.Clear();
            }
        }

        public static List<movie> requested_movie_list = new List<movie>();
    }



    class movie
    {
        public int movieid;
        public string title;
        public int year;
        public string rated;
        string released;
        int runtime;
        string genre;
        string director;
        string writer;
        string plot;
        string country;
        string poster;
        float rating_imdb;
        int rating_rt;
        int rating_metacritic;
        int rating_metascore;
        int imdb_votes;
        string imdbid;
        string type1;
        string production;

        public movie(int movieid, string title, int year, string rated, string released, int runtime, string genre, string director, string writer, string plot, string country, string poster, float rating_imdb, int rating_rt, int rating_metacritic, int rating_metascore, int imdb_votes, string imdbid, string type1, string production)
        {
            Movieid = movieid;
            Title = title;
            Year = year;
            Rated = rated;
            Released = released;
            Runtime = runtime;
            Genre = genre;
            Director = director;
            Writer = Writer;
            Plot = plot;
            Country = country;
            Poster = poster;
            Rating_imdb = rating_imdb;
            Rating_metacritic = rating_metacritic;
            Rating_metascrore = rating_metascore;
            Rating_rt = rating_rt;
            Imdbid = imdbid;
            Imdb_votes = imdb_votes;
            Type1 = type1;
            Production = production;
        }

        public int Movieid
        {
            get { return movieid; }
            set { movieid = value; }
        }

        public string Title { get; set; }
        public int Year{ get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public int Runtime{ get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Plot { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public float Rating_imdb { get; set; }
        public int Rating_rt { get; set; }
        public int Rating_metacritic { get; set; }
        public int Rating_metascrore { get; set; }
        public int Imdb_votes { get; set; }
        public string Imdbid { get; set; }
        public string Type1 { get; set; }
        public string Production { get; set; }

        

    }


    class actor
    {
        int actorid;
        string forename;
        string surname;
    }

}
