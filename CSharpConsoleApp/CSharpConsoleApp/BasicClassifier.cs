using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsoleApp
{
    public class BasicClassifier : IClassifier
    {
        private IEnumerable<Observation> _data;
        private readonly IDistance _distance;
        
        public BasicClassifier(IDistance distance)
        {
            this._distance = distance;
        }

        public void Train(IEnumerable<Observation> trainingSet)
        {
            this._data = trainingSet;
        }

        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = Double.MaxValue;

            foreach (Observation obs in this._data)
            {
                var dist = this._distance.Between(obs.Pixels, pixels);
                if (dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }

            return currentBest.Label;
        }
    }
}
