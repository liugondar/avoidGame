using AvoidMaster.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvoidMaster.Bus
{
     public class ScoreManager
    {
        private static string fileName = "scores.xml";
        public List<Score> Scores { get; private set; }
        public List<Score> HighScores { get; private set; }
        public ScoreManager() : this(new List<Score>()) { }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
            UpdateHighScores();
        }

        private void UpdateHighScores()
        {
            HighScores = Scores.Take(5).ToList();
        }
        public void Add(Score score)
        {
            Scores.Add(score);
            Scores.OrderByDescending(c => c.Value).ToList();

            UpdateHighScores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(fileName))
                return new ScoreManager();

            using (var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                var secrilizer = new XmlSerializer(typeof(List<Score>));

                var scores = ((List<Score>)secrilizer.Deserialize(reader));
                return new ScoreManager(scores);
            }
        }
        
        public static void Save(ScoreManager scoreManager)
        {
            using (var writter=new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                var secrilizer = new XmlSerializer(typeof(List<Score>));

                secrilizer.Serialize(writter, scoreManager.Scores);
            }
        }

    }
}
