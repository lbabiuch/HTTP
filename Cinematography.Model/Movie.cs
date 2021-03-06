using System;

namespace Cinematography.Model
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string TitlePl { get; set; }
        public string TitleEng { get; set; }
        public string ReleaseDate { get; set; }
        public Person Director { get; set; }
        public string Categories { get; set; }
        public Person[] Cast { get; set; }

        public Movie()
        {
        }

        public Movie(Guid id, string titlePl, string titleEng, string releaseDate, Person director, string categories, Person[] cast)
        {
            Id = id;
            TitlePl = titlePl;
            TitleEng = titleEng;
            ReleaseDate = releaseDate;
            Director = director;
            Categories = categories;
            Cast = cast;
        }
    }
}
