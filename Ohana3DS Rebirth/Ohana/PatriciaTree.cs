using System.Collections.Generic;

namespace Ohana3DS_Rebirth.Ohana
{
    class PatriciaTree
    {
        public class node
        {
            public int index;
            public int referenceBit;
            public string name;
            public node left, right;
        }
        public int nodeCount;
        public int maxLength;
        public List<node> nodes = new List<node>();
        public node rootNode = new node();

        public PatriciaTree(List<string> keys)
        {
            rootNode.left = rootNode;
            rootNode.right = rootNode;
            rootNode.referenceBit = -1;
            foreach (string key in keys) if (key.Length > maxLength) maxLength = key.Length;
            foreach (string key in keys) nodes.Add(insert(key));
        }

        private node insert(string key)
        {
            node rootNode = this.rootNode;
            node leftNode = rootNode.left;
            int bit = (maxLength << 3) - 1;
            while (rootNode.referenceBit > leftNode.referenceBit)
            {
                rootNode = leftNode;
                if (getBit(key, leftNode.referenceBit))
                    leftNode = leftNode.right;
                else
                    leftNode = leftNode.left;
            }
            while (getBit(leftNode.name, bit) == getBit(key, bit)) bit--;

            rootNode = this.rootNode;
            leftNode = rootNode.left;
            while ((rootNode.referenceBit > leftNode.referenceBit) && (leftNode.referenceBit > bit))
            {
                rootNode = leftNode;
                if (getBit(key, leftNode.referenceBit))
                    leftNode = leftNode.right;
                else
                    leftNode = leftNode.left;
            }

            node output = new node();
            output.name = key;
            output.referenceBit = bit;
            if (getBit(key, bit))
            {
                output.left = leftNode;
                output.right = output;
            }
            else
            {
                output.left = output;
                output.right = leftNode;
            }
            output.index = ++nodeCount;
            return output;
        }

        private bool getBit(string name, int bit)
        {
            int position = bit >> 3;
            int charBit = bit & 7;
            if (name == null || position >= name.Length) return false;
            return ((name[position] >> charBit) & 1) > 0;
        }
    }
}
