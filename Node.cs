using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomForest
{
    public class Node : DecisionTree
    {
        private string attribute;
        private Dictionary<string, DecisionTree> branches;

        public Node(string attribute)
        {
            this.attribute = attribute;
        }

        public string Attribute
        {
            get
            {
                return attribute;
            }

            set
            {
            }
        }

        public void AddBranch(string value, DecisionTree branch)
        {
            branches.Add(value, branch);
        }

        public override string Decide(Sample sample)
        {
            return branches[sample.Atributes[attribute]].Decide(sample);
        }
    }
}