﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomForest
{
    public class Leaf : DecisionTree
    {
        public string classLabel;

        public Leaf(string label)
        {
            classLabel = label;
        }

        public string ClassLabel
        {
            get
            {
                return classLabel;
            }

            set
            {
            }
        }

        public override string Decide(Sample sample)
        {
            return classLabel;
        }

        public string Decide()
        {
            return classLabel;
        }
    }
}