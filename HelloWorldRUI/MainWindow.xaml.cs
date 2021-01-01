
using MySql.Data.MySqlClient;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Forms;
using System.Windows.Input;

using HelloWorldRUI.DBHelper;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Text.RegularExpressions;


namespace HelloWorldRUI
{
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();


            DBHelper.DBHelper.EstablishConnection();
            /*
            //create mysql connection 
            string connection_info = "server=localhost;user id=root;persistsecurityinfo=True;database=movies;password=root";
            //string connection_info = "SERVER=localhost,DATABASE=movies;UID=root;PASSWORD=root";
            MySqlConnection connection = new MySqlConnection(connection_info);

            //   Database example and testing
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM movies.movies where title = \"interstellar\" limit 1", connection);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            //Console.WriteLine(cmd.ExecuteReader());

            Console.WriteLine(dt.Rows.Count);
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
            */

            //create Viewmodel
            ViewModel = new AppViewModel();

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, viewModel => viewModel.Lang,
                    view => view.LangTextBlock.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, viewModel => viewModel.Greeting,
                    view => view.GreetingTextBlock.Text).DisposeWith(d);
                

                this.BindCommand(ViewModel, x => x.Searchbutton, v => v.button_run_search)
                    .DisposeWith(d);
                this.BindCommand(ViewModel, x => x.FilterSearchbutton, v => v.Filtersearchbutton)
                    .DisposeWith(d);
                this.Bind(ViewModel, vm => vm.searchtext, v => v.searchtextbox.Text).DisposeWith(d);
                
                //A complete binding is created as following: the field is created in the view - a reactive variable in the viewmodel is created 
                                //                          - the 2 are connected by binding this.bind - a whenanyvalue is created that fires on changes and changes the variable in the class
                                //                          - in the class the changed variable is processed - when a button is pressed the changed variable is used

                //movie posters and titles binding
                this.OneWayBind(ViewModel, vm => vm.button_label0, v => v.button_label0.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image0, v => v.button_image0.Source).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_label1, v => v.button_label1.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image1, v => v.button_image1.Source).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_label2, v => v.button_label2.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image2, v => v.button_image2.Source).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_label3, v => v.button_label3.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image3, v => v.button_image3.Source).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_label4, v => v.button_label4.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image4, v => v.button_image4.Source).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_label5, v => v.button_label5.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.button_image5, v => v.button_image5.Source).DisposeWith(d);
                
                //results found binding
                this.OneWayBind(ViewModel, vm => vm.Resltsfound_text, v => v.Resltsfound_text.Text).DisposeWith(d);

                //type1 bindings
                this.Bind(ViewModel, vm => vm.Movies_checkbox, v => v.Movies_checkbox.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Series_checkbox, v => v.Series_checkbox.IsChecked).DisposeWith(d);

                //genres bindings
                this.Bind(ViewModel, vm => vm.Checkbox_genre_and_or, v => v.Checkbox_genre_and_or.IsChecked).DisposeWith(d);

                this.Bind(ViewModel, vm => vm.Checkbox_genre_action, v => v.Checkbox_genre_action.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Adult, v => v.Checkbox_genre_Adult.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Biography, v => v.Checkbox_genre_Biography.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Documentary, v => v.Checkbox_genre_Documentary.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Fantasy, v => v.Checkbox_genre_Fantasy.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Game_Show, v => v.Checkbox_genre_Game_Show.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_History, v => v.Checkbox_genre_History.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Mystery, v => v.Checkbox_genre_Mystery.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Reality_TV, v => v.Checkbox_genre_Reality_TV.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Romance, v => v.Checkbox_genre_Romance.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Western, v => v.Checkbox_genre_Western.IsChecked).DisposeWith(d);
                
                
                this.Bind(ViewModel, vm => vm.Checkbox_genre_adventure, v => v.Checkbox_genre_adventure.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Animation, v => v.Checkbox_genre_Animation.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_comedy, v => v.Checkbox_genre_comedy.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Crime, v => v.Checkbox_genre_Crime.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Drama, v => v.Checkbox_genre_Drama.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Horror, v => v.Checkbox_genre_Horror.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_SciFi, v => v.Checkbox_genre_SciFi.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Sport, v => v.Checkbox_genre_Sport.IsChecked).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Checkbox_genre_Thriller, v => v.Checkbox_genre_Thriller.IsChecked).DisposeWith(d);

                //year binding
                this.Bind(ViewModel, vm => vm.Numberbox_year_from, v => v.Numberbox_year_from.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Numberbox_year_to, v => v.Numberbox_year_to.Text).DisposeWith(d);

                //rating binding
                this.Bind(ViewModel, vm => vm.rating_imd, v => v.slValue_imd.Value).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.rating_rt, v => v.slValue_rt.Value).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.rating_metacritic, v => v.slValue_metacritic.Value).DisposeWith(d);
            });
            

        }
        public void NumberValidationyear1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
    }
    
}



// Normally the ViewModel goes in a separate file
namespace HelloWorldRUI
{
    public class AppViewModel : ReactiveObject
    {

        string noimage = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string Greeting { get; set; } = "Greeting";

        
        [Reactive] public string searchtext { get; set; } = "type title";

        [Reactive] public int Resltsfound_text { get; set; }

        //all the fileds to show the movies
        [Reactive] public string button_label0 { get; set; } = "no title";
        [Reactive] public string button_image0 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label1 { get; set; } = "no title";
        [Reactive] public string button_image1 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label2 { get; set; } = "no title";
        [Reactive] public string button_image2 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label3 { get; set; } = "no title";
        [Reactive] public string button_image3 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label4 { get; set; } = "no title";
        [Reactive] public string button_image4 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label5 { get; set; } = "no title";
        [Reactive] public string button_image5 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public string button_label6 { get; set; } = "no title";
        [Reactive] public string button_image6 { get; set; } = @"E:\Downloads\HelloWorldRUI-master\HelloWorldRUI-master\HelloWorldRUI\Images\no-image.png";

        [Reactive] public bool Movies_checkbox { get; set; }
        [Reactive] public bool Series_checkbox { get; set; }

        [Reactive] public bool Checkbox_genre_and_or { get; set; }
        [Reactive] public bool Checkbox_genre_action {get ; set ;}
       [Reactive] public bool Checkbox_genre_adventure { get; set;}
       [Reactive] public bool Checkbox_genre_comedy { get; set; }
       [Reactive] public bool Checkbox_genre_SciFi { get; set; }
       [Reactive] public bool Checkbox_genre_Crime { get; set; }
       [Reactive] public bool Checkbox_genre_Drama { get; set; }
       [Reactive] public bool Checkbox_genre_Thriller { get; set; }
       [Reactive] public bool Checkbox_genre_Animation { get; set; }
       [Reactive] public bool Checkbox_genre_Sport { get; set; }
       [Reactive] public bool Checkbox_genre_Horror { get; set; }
       [Reactive] public bool Checkbox_genre_History { get; set; }
       [Reactive] public bool Checkbox_genre_Biography { get; set; }
       [Reactive] public bool Checkbox_genre_Romance { get; set; }
       [Reactive] public bool Checkbox_genre_Fantasy { get; set; }
       [Reactive] public bool Checkbox_genre_Mystery { get; set; }
       [Reactive] public bool Checkbox_genre_Documentary { get; set; }
       [Reactive] public bool Checkbox_genre_Western { get; set; }
       [Reactive] public bool Checkbox_genre_Adult { get; set; }
       [Reactive] public bool Checkbox_genre_Game_Show { get; set; }
       [Reactive] public bool Checkbox_genre_Reality_TV { get; set; }

        [Reactive] public int Numberbox_year_from { get; set; } = 0;
        [Reactive] public int Numberbox_year_to { get; set; } = 2020;

        [Reactive] public double rating_imd { get; set; } = -1;
        [Reactive] public int rating_rt { get; set; } = -1;
        [Reactive] public int rating_metacritic { get; set; } = -1;

        public ReactiveCommand<bool, System.Reactive.Unit> run_filter_search { get;  set; }
        public ReactiveCommand<Int32, System.Reactive.Unit> run_filter_search_int32 { get; set; }

        public ReactiveCommand<Unit, Unit> Searchbutton;

        public ReactiveCommand<Unit, Unit> FilterSearchbutton;



        public extern string Lang { [ObservableAsProperty] get; }
        //private readonly ObservableAsPropertyHelper<string> _lang;
        //public string Lang => _lang.Value;

        public static void printtest()
        {
            Console.WriteLine("here i am, button pusehd");
        }

        public void ShowMovies()
        {
            if (DBrequests.movie_list.Count() == 0)
            {
                //no movie found
                //inform the user
                MessageBoxResult result = System.Windows.MessageBox.Show("No movies found");

                //BitmapImage img = new BitmapImage();
                //img.UriSource = new Uri(@"pack://application:,,,/FooApplication;component/Images/cat.png");
                //button_image0 = img;

            }
           
            else if (DBrequests.movie_list.Count() >= 1)
            {
                Console.WriteLine("more than one result");
                button_label0 = DBrequests.movie_list[0].Title;
                if (DBrequests.movie_list[0].Poster == "Nan")
                {
                    button_image0 = noimage;
                }
                else
                {
                    button_image0 = DBrequests.movie_list[0].Poster;
                }

                if (DBrequests.movie_list.Count() > 1)
                {
                    button_label1 = DBrequests.movie_list[1].Title;
                    if (DBrequests.movie_list[1].Poster == "Nan")
                    {
                        button_image1 = noimage;
                    }
                    else
                    {
                        button_image1 = DBrequests.movie_list[1].Poster;
                    }
                }
                if (DBrequests.movie_list.Count() > 2)
                {
                    button_label2 = DBrequests.movie_list[2].Title;
                    if (DBrequests.movie_list[2].Poster == "Nan")
                    {
                        button_image2 = noimage;
                    }
                    else
                    {
                        button_image2 = DBrequests.movie_list[2].Poster;
                    }
                }
                if (DBrequests.movie_list.Count() > 3)
                {
                    button_label3 = DBrequests.movie_list[3].Title;
                    if (DBrequests.movie_list[3].Poster == "Nan")
                    {
                        button_image3 = noimage;
                    }
                    else
                    {
                        button_image3 = DBrequests.movie_list[3].Poster;
                    }
                }
                if (DBrequests.movie_list.Count() > 4)
                {
                    button_label4 = DBrequests.movie_list[4].Title;
                    button_image4 = DBrequests.movie_list[4].Poster;
                }
                if (DBrequests.movie_list.Count() > 5)
                {
                    button_label5 = DBrequests.movie_list[5].Title;
                    button_image5 = DBrequests.movie_list[5].Poster;
                }

            }


        }

        public void Clear_movies()
        {
            //empty the list of movies first
            if (DBrequests.movie_list.Count() > 0)
            {
                //DBHelper.requestedmovies.requested_movie_list.Clear();
                DBrequests.Clear_movie_list();
                ShowMovies();
            }
        }

        public void search()
        {
            //set the title that will b e searched
            DBrequests.title = searchtext;
            DBrequests.search_multible();
        }

        public void search_filter()
        {
            DBrequests.filter_search();
        }

        public AppViewModel()
        {
            //this.Button1 => printtest();

            run_filter_search = ReactiveCommand.Create<bool>(x => {
                //Console.WriteLine("button pressed");
                //Console.WriteLine("genre action: " + DBrequests.genre_action);
                //Clear_movies();
                //search_filter();
                //ShowMovies();
            });

            run_filter_search_int32 = ReactiveCommand.Create<Int32>(x => {
                //Console.WriteLine("button pressed");
                //Console.WriteLine("genre action: " +DBrequests.genre_action);
                //Clear_movies();
                //search_filter();
                //ShowMovies();
            });

            FilterSearchbutton = ReactiveCommand.Create(() => {
                //Console.WriteLine("button pressed");
                Console.WriteLine("genre action: " + DBrequests.genre_action);
                Clear_movies();
                search_filter();
                ShowMovies();
            });

            Searchbutton = ReactiveCommand.Create(() =>{
                //Console.WriteLine("button pressed");
                Console.WriteLine("genre action: " + DBrequests.genre_action);
                Clear_movies();
                search();
                ShowMovies();
            });


            Dictionary<string, string> Greetings = new Dictionary<string, string>() {
                { "English", "Hello World!" },
                { "French", "Bonjour le monde!" },
                { "German", "Hallo Welt!" },
                { "Japanese", "Kon'nichiwa sekai!" },
                { "Spanish", "¡Hola Mundo!" },
            };
            //var keys = Greetings.Keys.ToArray();
            string[] keys = Greetings.Keys.ToArray();

            // select next language every 2 seconds (100 times)
            //_lang = Observable.Interval(TimeSpan.FromSeconds(2))
            Observable.Interval(TimeSpan.FromSeconds(2))
                .Take(100)
                .Select(_ => keys[(Array.IndexOf(keys, Lang) + 1) % keys.Count()])
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Lang, "Language");

            //this.WhenAnyValue(x => x.Button1)
                

            // update Greeting when language changes
            this.WhenAnyValue(x => x.Lang)
                .Where(lang => keys.Contains(lang))
                .Select(x => Greetings[x])
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => Greeting = x);

            //this.WhenAnyValue(x => x.button_image0).ObserveOn(RxApp.MainThreadScheduler);

            this.WhenAnyValue(x => x.button_label0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => Resltsfound_text = DBrequests.movie_list.Count);

            //idea to search without pressing the search button
            //this.WhenAnyValue(x => x.searchtext)
            //   .ObserveOn(RxApp.MainThreadScheduler)
            // .Where(x => !String.IsNullOrWhiteSpace(x))
            //  .Throttle(TimeSpan.FromSeconds(.95))
            // .Subscribe(x => search());

            this.WhenAnyValue(x => x.Movies_checkbox)
                .Subscribe(x => DBrequests.movies_checkbox = x);
            this.WhenAnyValue(x => x.Series_checkbox)
                .Subscribe(x => DBrequests.series_checkbox = x);

            double throttletime = 0.9;

            this.WhenAnyValue(x => x.Checkbox_genre_and_or)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_and_or)
                .Subscribe(x => DBrequests.genre_or = x);
            
            this.WhenAnyValue(x => x.Checkbox_genre_action)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_action)
                .Subscribe(x => DBrequests.genre_action = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Adult)
                .Subscribe(x => DBrequests.genre_adult = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Adult)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_adventure)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_adventure)
                .Subscribe(x => DBrequests.genre_adventure = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Animation)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Animation)
                .Subscribe(x => DBrequests.genre_animation = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Biography)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Biography)
                .Subscribe(x => DBrequests.genre_biography = x);
            this.WhenAnyValue(x => x.Checkbox_genre_comedy)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_comedy)
                .Subscribe(x => DBrequests.genre_comedy = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Crime)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Crime)
                .Subscribe(x => DBrequests.genre_crime = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Documentary)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Documentary)
                .Subscribe(x => DBrequests.genre_documentery = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Drama)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Drama)
                .Subscribe(x => DBrequests.genre_drama = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Fantasy)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Fantasy)
                .Subscribe(x => DBrequests.genre_fantasy = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Game_Show)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Game_Show)
                .Subscribe(x => DBrequests.genre_game_show = x);
            this.WhenAnyValue(x => x.Checkbox_genre_History)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_History)
                .Subscribe(x => DBrequests.genre_history = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Horror)
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Horror)
                .Subscribe(x => DBrequests.genre_horror = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Mystery)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Mystery)
                .Subscribe(x => DBrequests.genre_mystery = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Reality_TV)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Reality_TV)
                .Subscribe(x => DBrequests.genre_reality_tv = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Romance)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Romance)
                .Subscribe(x => DBrequests.genre_romance = x);
            this.WhenAnyValue(x => x.Checkbox_genre_SciFi)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_SciFi)
                .Subscribe(x => DBrequests.genre_scifi = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Sport)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Sport)
                .Subscribe(x => DBrequests.genre_sport = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Thriller)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Thriller)
                .Subscribe(x => DBrequests.genre_thriller = x);
            this.WhenAnyValue(x => x.Checkbox_genre_Western)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search);
            this.WhenAnyValue(x => x.Checkbox_genre_Western)
                .Subscribe(x => DBrequests.genre_western = x);

    
            //years
            
            this.WhenAnyValue(x => x.Numberbox_year_from)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search_int32);
                
            this.WhenAnyValue(x => x.Numberbox_year_from)
                .Subscribe(x => DBrequests.year_from = x);
            this.WhenAnyValue(x => x.Numberbox_year_to)
                .Throttle(TimeSpan.FromSeconds(throttletime))
                .InvokeCommand(run_filter_search_int32);
            this.WhenAnyValue(x => x.Numberbox_year_to)
                .Subscribe(x => DBrequests.year_to = x);


            //rating
            this.WhenAnyValue(x => x.rating_imd)
               .Subscribe(x => DBrequests.rating_imd = x);
            this.WhenAnyValue(x => x.rating_rt)
               .Subscribe(x => DBrequests.rating_rt = x);
            this.WhenAnyValue(x => x.rating_metacritic)
               .Subscribe(x => DBrequests.rating_metacritic = x);

        }
    }
}
