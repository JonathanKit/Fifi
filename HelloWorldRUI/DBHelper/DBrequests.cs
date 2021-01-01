using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorldRUI.DBHelper;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Mozilla;

namespace HelloWorldRUI.DBHelper
{
    class DBrequests
    {
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;
        private static string query;

        //run a function to change the movie list, the movie list is in the end the thing that is diplayed
        public static List<movie> movie_list = new List<movie>();

        //all variables that can be set for searching a movie
        public static string title;

        public static bool movies_checkbox;
        public static bool series_checkbox;


        //genres
        public static bool genre_or;

        public static bool genre_action;
        public static bool genre_adventure;
        public static bool genre_comedy;
        public static bool genre_scifi;
        public static bool genre_crime;
        public static bool genre_drama;
        public static bool genre_thriller;
        public static bool genre_animation;
        public static bool genre_sport;
        public static bool genre_horror;
        public static bool genre_history;
        public static bool genre_biography;
        public static bool genre_romance;
        public static bool genre_fantasy;
        public static bool genre_mystery;
        public static bool genre_documentery;
        public static bool genre_western;
        public static bool genre_adult;
        public static bool genre_game_show;
        public static bool genre_reality_tv;
        //....

        public static int year_from;
        public static int year_to;

        public static double rating_imd;
        public static int rating_rt;
        public static int rating_metacritic;

        public static string orderby = "";//"order by title";
        //public int max_limit = 50;

        //search with filters
        public static void filter_search()
        {
            query = "";
            query = $"select distinct * from movies LEFT JOIN movies_genre on (movies.movieID=movies_genre.movieID) " +
                $"LEFT JOIN genres ON (genres.genreID=movies_genre.genreID) where";


            //genre block
            if (genre_action){query = query + " genre_name =\"Action\"" + and_or(); }
            if (genre_adult) { query = query + " genre_name =\"adult\""+ and_or(); }
            if (genre_adventure) { query = query + " genre_name =\"adventure\"" + and_or(); }
            if (genre_animation) { query = query + " genre_name =\"animation\"" + and_or(); }
            if (genre_biography) { query = query + " genre_name =\"biography\"" + and_or(); }
            if (genre_comedy) { query = query + " genre_name =\"comedy\"" + and_or(); }
            if (genre_crime) { query = query + " genre_name =\"crime\"" + and_or(); }
            if (genre_documentery) { query = query + " genre_name =\"documentary\"" + and_or(); }
            if (genre_drama) { query = query + " genre_name =\"drama\"" + and_or(); }
            if (genre_fantasy) { query = query + " genre_name =\"fantasy\"" + and_or(); }
            if (genre_game_show) { query = query + " genre_name =\"game-show\"" + and_or(); }
            if (genre_history) { query = query + " genre_name =\"history\"" + and_or(); }
            if (genre_horror) { query = query + " genre_name =\"horror\"" + and_or(); }
            if (genre_mystery) { query = query + " genre_name =\"mystery\"" + and_or(); }
            if (genre_reality_tv) { query = query + " genre_name =\"reality-tv\"" + and_or(); }
            if (genre_romance) { query = query + " genre_name =\"romance\"" + and_or(); }
            if (genre_scifi) { query = query + " genre_name =\"sci-fi\"" + and_or(); }
            if (genre_sport) { query = query + " genre_name =\"sport\"" + and_or(); }
            if (genre_thriller) { query = query + " genre_name =\"thriller\"" + and_or(); }
            if (genre_western) { query = query + " genre_name =\"western\"" + and_or(); }
            string lastWord = query.Trim().Split(' ').Last();
            if (lastWord == "or")
            {
                query = ReplaceLast_and_or(query, "or", "and");
            }
            //end genres block

            

            //year block
            query = query + $" year>{year_from} and ";
            query = query + $" year<{year_to}";
            //year block end

            query = query + " and ";

            //type block
            if (movies_checkbox)
            {
                query = query + " type1 = \"movie\" or ";
            }
            if (series_checkbox)
            {
                query = query + " type1 = \"series\" and ";
            }
            lastWord = query.Trim().Split(' ').Last();
            if (lastWord == "or")
            {
                query = ReplaceLast_and_or(query, "or", " and ");
            }
            //type block end


            //rating block
            string rating_imd_string = rating_imd.ToString();
            rating_imd_string = ReplaceLast_and_or(rating_imd_string, ",", ".");
            query = query + $"rating_imd >= {rating_imd_string} and ";
            query = query + $"rating_rt >= {rating_rt} and ";
            query = query + $"rating_metacritic >= {rating_metacritic} and ";

            //end rating block


            //cleaning the ending of the query
            lastWord = query.Trim().Split(' ').Last();
            if (lastWord == "where")
            {
                query = ReplaceLast_and_or(query, "where", "");
            }
            lastWord = query.Trim().Split(' ').Last();
            if (lastWord == "or")
            {
                query = ReplaceLast_and_or(query, "or", "");
            }
            if (lastWord == "and")
            {
                query = ReplaceLast_and_or(query, "and", "");
            }

            
           

            //$" where (genre_name=\"comedy\" or genre_name=\"horror\") order by title limit 30; ";

            query = query + "limit 20";

            retrieveMovie();
        }

        private static string ReplaceLast_and_or(string Source, string Find, string Replace = "")
        {

            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            string result = Source.Remove(place, Find.Length).Insert(place, Replace);

            return result;
        }

        private static string and_or()
        {
            if (genre_or)
            {
                return " or ";
            }
            else
            {
                return " or ";
            }
        }

        //creates the query to seach for a title based on a substring
        public static void search_multible()
        {
            query = "";
            query = $"SELECT * FROM movies.movies where title like \"%{title}%\"; ";

            /*
            query = @"SELECT * FROM movies.movies where title = """;
            query = query + search_title;
            query = query + @""" limit 1;";
            */

            retrieveMovie();
        }

        //creates the query to seach for one title
        public static void search()
        {
            query = "";
            query = $"SELECT * FROM movies.movies where title = \"{title}\" limit 1; ";

            /*
            query = @"SELECT * FROM movies.movies where title = """;
            query = query + search_title;
            query = query + @""" limit 1;";
            */
            retrieveMovie();
        }

        public static void retrieveMovie()
        {
            //get query changed by the function calling retrieve movie
            Console.WriteLine(query);

            //useless username, it wont be used when calling runquery
            string username = "nousername";
            cmd = DBHelper.RunQuery(query, username);

            //first delete all existing movies in the movie list
            movie_list.Clear();
            movie aMovie = null;
            
            if (cmd != null)
            {
                dt = new DataTable();

                //run query
                dt.Load(cmd.ExecuteReader());
                DBHelper.connection.Close();
                
                //run the loop to extract all the movie information, pack them in movies and save them ina  list of movies
                foreach (DataRow dr in dt.Rows)
                {

                    int get_movieid = (int)dr["movieID"];
                    string get_title = dr["title"].ToString();
                    int get_year = (int)dr["year"];
                    string get_rated = dr["rated"].ToString();
                    string get_released = dr["released"].ToString();
                    int get_runtime = (int)dr["runtime"];
                    string get_genre = dr["genre"].ToString();
                    string get_director = dr["director"].ToString();
                    string get_writer = dr["writer"].ToString();
                    string get_plot = dr["plot"].ToString();
                    string get_country = dr["country"].ToString();
                    string get_poster = dr["poster"].ToString();
                    float get_rating_imdb = (float)dr["rating_imdb"];
                    int get_rating_rt = (int)dr["rating_rt"];
                    int get_rating_metacritic = (int)dr["rating_metacritic"];
                    int get_rating_metascore = (int)dr["rating_metascore"];
                    int get_imdb_votes = (int)dr["imdbVotes"];
                    string get_imdbid = dr["imdbid"].ToString();
                    string get_type1 = dr["type1"].ToString();
                    string get_production = dr["production"].ToString();

                    //rated,released,runtime,genre,director,writer,plot,country,poster,rating_imdb,rating_rt,
                    //rating_metacritic,rating_metascore,imdb_votes,imdbid,type1,production,





                    aMovie = new movie(get_movieid, get_title, get_year, get_rated, get_released, get_runtime, get_genre, get_director,
                                        get_writer, get_plot, get_country, get_poster, get_rating_imdb, get_rating_rt, get_rating_metacritic,
                                        get_rating_metascore, get_imdb_votes, get_imdbid, get_type1, get_production
                                        );
                    movie_list.Add(aMovie);
                }
            }
            
        }

        public static void Clear_movie_list()
        {
            foreach(movie movie in movie_list)
            {
                movie.Title = "no title";
                movie.Movieid = 0;
                movie.Year = 0;
                movie.Rated = "";
                //...
                movie.Poster = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";
            }
                
        }
    }
}
