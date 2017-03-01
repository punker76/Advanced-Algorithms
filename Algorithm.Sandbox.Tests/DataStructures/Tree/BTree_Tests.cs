﻿using Algorithm.Sandbox.DataStructures.Tree;
using Algorithm.Sandbox.Tests.DataStructures.Tree.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sandbox.Tests.DataStructures.Tree
{
    [TestClass]
    public class BTree_Tests
    {
        /// </summary>
        [TestMethod]
        public void BTree_Smoke_Test()
        {
            //insert test
            var tree = new AsBTree<int>(3);

            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(21);
            tree.Insert(9);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(10);
            tree.Insert(12);
            tree.Insert(4);
            tree.Insert(8);


            ////delete
            tree.Delete(2);
            tree.Delete(21);
            tree.Delete(10);
            tree.Delete(3);
            tree.Delete(4);
            tree.Delete(7);
            tree.Delete(9);
            tree.Delete(1);
            tree.Delete(5);
            tree.Delete(8);
            tree.Delete(13);
            tree.Delete(12);

            Assert.AreEqual(tree.Count, 0);


        }

        [TestMethod]
        public void BTree_AccuracyTest()
        {

            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                            .OrderBy(x => rnd.Next())
                            .ToList();

            var order = 3;
            var tree = new AsBTree<int>(order);

            for (int i = 0; i < nodeCount; i++)
            {

                tree.Insert(randomNumbers[i]);

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = Math.Ceiling(Math.Log((i + 2) / 2, (int)Math.Ceiling((double)order / 2)));

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == i + 1);

                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(tree.HasItem(randomNumbers[i]));
            }

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                            .OrderBy(x => rnd.Next())
                            .ToList();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);

                var actualMaxHeight = BTreeTester.GetMaxHeight(tree.Root);
                var actualMinHeight = BTreeTester.GetMinHeight(tree.Root);

                Assert.IsTrue(actualMaxHeight == actualMinHeight);

                //https://en.wikipedia.org/wiki/B-tree#Best_case_and_worst_case_heights
                var theoreticalMaxHeight = Math.Ceiling(Math.Log((nodeCount - i + 2) / 2, (int)Math.Ceiling((double)order / 2)));

                Assert.IsTrue(actualMaxHeight <= theoreticalMaxHeight);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }


        [TestMethod]
        public void BTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var rnd = new Random();
            var randomNumbers = Enumerable.Range(1, nodeCount)
                                .OrderBy(x => rnd.Next())
                                .ToList();

            var tree = new AsBTree<int>(3);

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(randomNumbers[i]);
                Assert.IsTrue(tree.Count == i + 1);
            }

            //shuffle again before deletion tests
            randomNumbers = Enumerable.Range(1, nodeCount)
                               .OrderBy(x => rnd.Next())
                               .ToList();


            for (int i = 0; i < nodeCount; i++)
            {
                tree.Delete(randomNumbers[i]);
                Assert.IsTrue(tree.Count == nodeCount - 1 - i);
            }

            Assert.IsTrue(tree.Count == 0);
        }
    }
}