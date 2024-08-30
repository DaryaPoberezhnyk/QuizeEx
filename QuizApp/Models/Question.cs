using System.Collections.Generic;

namespace QuizApp.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
public class Quiz
    {
        public List<Question> Questions { get; set; }
        public List<int> UserAnswers { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int TimeRemaining { get; set; }
        private DateTime _startTime { get; set; }

        public Quiz()
        {
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "Which fictional city is the home of Batman?",
                    Options = new List<string> { "Metropolis", "Gotham", "Star City", "Central City" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "In which year did the first man land on the moon?",
                    Options = new List<string> { "1967", "1968", "1969", "1970" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which mythological creature is said to have a horse's body and a fish's tail?",
                    Options = new List<string> { "Griffin", "Centaur", "Hippocampus", "Phoenix" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "What is the capital of the fictional country of Wakanda?",
                    Options = new List<string> { "Shuri", "T'Challa", "Birnin Zana", "Zamunda" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which tech entrepreneur is known for founding SpaceX?",
                    Options = new List<string> { "Steve Jobs", "Elon Musk", "Jeff Bezos", "Mark Zuckerberg" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the rarest blood type in the world?",
                    Options = new List<string> { "O", "A", "B", "AB-Negative" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "Which country gifted the Statue of Liberty to the USA?",
                    Options = new List<string> { "United Kingdom", "Canada", "France", "Germany" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "In which movie did Leonardo DiCaprio famously say, 'I'm the king of the world!'?",
                    Options = new List<string> { "Inception", "Titanic", "The Revenant", "Catch Me If You Can" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the hardest substance in the human body?",
                    Options = new List<string> { "Bone", "Enamel", "Cartilage", "Tendon" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which planet in our solar system is the largest?",
                    Options = new List<string> { "Earth", "Jupiter", "Saturn", "Neptune" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which is the only mammal capable of true flight?",
                    Options = new List<string> { "Squirrel", "Bat", "Penguin", "Ostrich" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which word is used to describe an octopus escaping from a jar in just 60 seconds?",
                    Options = new List<string> { "Escapology", "Cephalopod", "Tentacology", "Houdini" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "What is the most spoken language in the world?",
                    Options = new List<string> { "Spanish", "English", "Mandarin", "Hindi" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which element is essential for the formation of steel?",
                    Options = new List<string> { "Carbon", "Oxygen", "Nitrogen", "Hydrogen" },
                    CorrectAnswerIndex = 0
                },
                new Question
                {
                    Text = "Which artist is famous for his painting 'The Starry Night'?",
                    Options = new List<string> { "Claude Monet", "Vincent van Gogh", "Pablo Picasso", "Salvador Dalí" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which is the smallest planet in our solar system?",
                    Options = new List<string> { "Mars", "Venus", "Mercury", "Pluto" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which fruit is known as the 'king of fruits' in Southeast Asia?",
                    Options = new List<string> { "Mango", "Durian", "Banana", "Pineapple" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which famous landmark has the nickname 'The Big Ben'?",
                    Options = new List<string> { "Eiffel Tower", "Statue of Liberty", "Great Wall", "Elizabeth Tower" },
                    CorrectAnswerIndex = 3
                },
                new Question
                {
                    Text = "In which city would you find the ancient Acropolis?",
                    Options = new List<string> { "Rome", "Athens", "Cairo", "Istanbul" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the main ingredient in traditional Japanese sushi?",
                    Options = new List<string> { "Noodles", "Fish", "Rice", "Seaweed" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which continent is known for having the most species of reptiles?",
                    Options = new List<string> { "Asia", "Africa", "Australia", "South America" },
                    CorrectAnswerIndex = 2
                },
                new Question
                {
                    Text = "Which novel begins with the line 'Call me Ishmael'?",
                    Options = new List<string> { "1984", "Moby-Dick", "The Catcher in the Rye", "To Kill a Mockingbird" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "Which bird is known for its ability to mimic human speech?",
                    Options = new List<string> { "Eagle", "Parrot", "Owl", "Raven" },
                    CorrectAnswerIndex = 1
                },
                new Question
                {
                    Text = "What is the capital city of New Zealand?",
                    Options = new List<string> { "Auckland", "Christchurch", "Wellington", "Dunedin" },
                    CorrectAnswerIndex = 2
                },
            };

            // Shuffle questions
            var rng = new Random();
            Questions = Questions.OrderBy(q => rng.Next()).ToList();

            UserAnswers = new List<int>(new int[Questions.Count]);
            CurrentQuestionIndex = 0;
            TimeRemaining = 30;
            _startTime = DateTime.Now;

        }

        public Question GetCurrentQuestion()
        {
            if (CurrentQuestionIndex < Questions.Count)
            {
                return Questions[CurrentQuestionIndex];
            }
            return null;
        }
        public void UpdateTime()
        {
            var elapsed = (DateTime.Now - _startTime).TotalSeconds;
            TimeRemaining = Math.Max(0, 30- (int)elapsed);
            if (TimeRemaining <= 0)
            {
                MoveToNextQuestion();
            }
        }
        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
            _startTime = DateTime.Now; 
            if (CurrentQuestionIndex >= Questions.Count)
            {
                CurrentQuestionIndex = Questions.Count - 1;
            }
        }
    }
}
