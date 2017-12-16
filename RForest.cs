using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RandomForest
{
    public class RForest
    {
        public List<DecisionTree> trees;

        public RForest(SamplesContainer samples, int treeCount)
        {
            Random random = new Random();
            int m = Convert.ToInt32(Math.Truncate(Math.Sqrt(samples.attributes.Count())));
            trees = new List<DecisionTree>();
            for (int i = 0; i < treeCount; i++)
            {
                List<Sample> selectedSamples = new List<Sample>();
                for (int j = 0; j < samples.samplesList.Count; j++)
                    selectedSamples.Add(samples.samplesList[random.Next(samples.samplesList.Count)]);
                List<string> attributes = new List<string>();
                int m1 = m;
                for (int j = 0; j < samples.attributes.Count; j++)
                {
                    if (random.Next() % (samples.attributes.Count - j) < m1)
                    {
                        attributes.Add(samples.attributes[j]);
                        m1--;
                    }
                }
                SamplesContainer samplesForTree = new SamplesContainer(selectedSamples);
                samplesForTree.attributes = attributes;
                samplesForTree.samplesDomain = samples.samplesDomain;
                trees.Add(C4_5.TreeInduction(samplesForTree));
            }
        }
    }
}