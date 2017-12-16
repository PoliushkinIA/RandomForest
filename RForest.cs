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

                trees.Add(TreeInduction(samplesForTree));
            }
        }

        private DecisionTree TreeInduction(SamplesContainer samples)
        {
            // If set contains samples of only one class return the leaf
            if (samples.samplesList.Select(p => p.ClassLabel).Distinct().Count() == 1)
            {
                return new Leaf(samples.samplesList[0].ClassLabel);
            }
            // Calculate Info(T)
            double infoT = Info(samples);
            // For each attribure calculate Gain(X)
            Dictionary<string, double> gain = new Dictionary<string, double>();
            foreach (string attribute in samples.attributes)
            {
                double info = 0;
                int powerT = samples.samplesList.Count;
                foreach (string value in samples.samplesDomain[attribute])
                {
                    SamplesContainer subset = new SamplesContainer(samples.samplesList.Where(p => p.GetAttribute(attribute) == value).ToList());
                    info += subset.samplesList.Count / powerT * Info(subset);
                }
                gain.Add(attribute, infoT - info);
            }
            // Find the attribute which maximize gain
            string selectedAttribute = gain.ToList().Find(p => p.Value == gain.Values.Max()).Key;
            // Make the tree node
            Node node = new Node(selectedAttribute);
            // For each attribute value induct the subtree
            foreach (string value in samples.samplesDomain[selectedAttribute])
            {
                // Get the subset
                List<Sample> subset = samples.samplesList.Where(p => p.Atributes[selectedAttribute] == value).ToList();
                if (subset.Count() == 0)
                {
                    // If subset for this value is empty the subtree is a leaf with classmark of most examples in our set
                    node.AddBranch(value, new Leaf(samples.samplesList.GroupBy(p => p.ClassLabel).Where(p => p.Count() == samples.samplesList.GroupBy(q => q.ClassLabel).Max(r => r.Count())).First().First().ClassLabel));
                }
                else
                {
                    // Otherwise recursively create a subtree for a subset
                    SamplesContainer subsetContainer = new SamplesContainer(subset);
                    subsetContainer.samplesDomain = samples.samplesDomain;
                    node.AddBranch(value, TreeInduction(subsetContainer));
                }
            }
            return node;
        }

        private double Info(SamplesContainer samples)
        {
            double info = 0;
            foreach (string c in samples.classLabels)
            {
                double p = (double)samples.samplesList.Where(q => q.ClassLabel.Equals(c)).Count() / samples.samplesList.Count();
                info -= p * Math.Log(p, 2);
            }
            return info;
        }
    }
}