﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Compatibility;
using static System.Console;
using static SimdSpike.Utilities;

namespace SimdSpike {
    public class PerformanceTests {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        public static void TestIntArrayAdditionFunctions(int testSetSize) {
            WriteLine();
            Write("Testing int array addition, generating test data...");
            var intsOne = GetRandomIntArray(testSetSize);
            var intsTwo = GetRandomIntArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                stopwatch.Restart();
                var result = IntSimdProcessor.NaiveSumFunc(intsOne, intsTwo);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimeMs}ms (last value = {result.Last()}).");

                stopwatch.Restart();
                result = IntSimdProcessor.HWAcceleratedSumFunc(intsOne, intsTwo);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimeMs}ms (last value = {result.Last()}).");
            }

            WriteLine("Int array addition:");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestInPlaceFloatAddition(int testSetSize) {
            WriteLine();
            Write("Testing float array addition, generating test data...");
            var floatsOne = GetRandomFloatArray(testSetSize);
            var floatsTwo = GetRandomFloatArray(testSetSize);
            WriteLine(" done, testing...");
            
            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                var floatsOneCopy = new float[floatsOne.Length];

                floatsOne.CopyTo(floatsOneCopy, 0);
                stopwatch.Restart();
                FloatSimdProcessor.HwAcceleratedSumInPlace(floatsOneCopy, floatsTwo);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"HW accelerated addition took: {hwTimeMs}ms (last value = {floatsOneCopy[floatsOneCopy.Length - 1]}).");

                floatsOne.CopyTo(floatsOneCopy, 0);
                stopwatch.Restart();
                FloatSimdProcessor.NaiveSumInPlace(floatsOneCopy, floatsTwo);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive addition took:          {naiveTimeMs}ms (last value = {floatsOneCopy[floatsOneCopy.Length - 1]}).");
            }

            WriteLine("Testing float array addition");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestInPlaceUShortAddition(int testSetSize) {
            WriteLine();
            Write("Testing ushort array in place addition, generating test data...");
            var ushortsOne = GetRandomUShortArray(testSetSize);
            var ushortsTwo = GetRandomUShortArray(testSetSize);
            WriteLine(" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                var ushortsOneCopy = new ushort[ushortsOne.Length];

                ushortsOne.CopyTo(ushortsOneCopy, 0);
                stopwatch.Restart();
                UShortSimdProcessor.HwAcceleratedSumInPlaceUnchecked(ushortsOneCopy, ushortsTwo);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"HW accelerated addition took: {hwTimeMs}ms (last value = {ushortsOneCopy[ushortsOneCopy.Length - 1]}).");

                ushortsOne.CopyTo(ushortsOneCopy, 0);
                stopwatch.Restart();
                UShortSimdProcessor.NaiveSumInPlaceUnchecked(ushortsOneCopy, ushortsTwo);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive addition took:          {naiveTimeMs}ms (last value = {ushortsOneCopy[ushortsOneCopy.Length - 1]}).");
            }

            WriteLine("Testing ushort array addition");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestInPlaceIntAddition(int testSetSize) {
            WriteLine();
            Write("Testing int array in place addition, generating test data...");
            var ushortsOne = GetRandomIntArray(testSetSize);
            var ushortsTwo = GetRandomIntArray(testSetSize);
            WriteLine(" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                var ushortsOneCopy = new int[ushortsOne.Length];

                ushortsOne.CopyTo(ushortsOneCopy, 0);
                stopwatch.Restart();
                IntSimdProcessor.HwAcceleratedSumInPlace(ushortsOneCopy, ushortsTwo);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"HW accelerated addition took: {hwTimeMs}ms (last value = {ushortsOneCopy[ushortsOneCopy.Length - 1]}).");

                ushortsOne.CopyTo(ushortsOneCopy, 0);
                stopwatch.Restart();
                IntSimdProcessor.NaiveSumInPlace(ushortsOneCopy, ushortsTwo);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive addition took:          {naiveTimeMs}ms (last value = {ushortsOneCopy[ushortsOneCopy.Length - 1]}).");
            }

            WriteLine("Testing int array addition in place");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestUShortAdditionIntoResultsArray(int testSetSize) {
            WriteLine();
            Write("Testing ushort array addition into results array, generating test data...");
            var ushortsOne = GetRandomUShortArray(testSetSize);
            var ushortsTwo = GetRandomUShortArray(testSetSize);
            var result = new ushort[testSetSize];
            WriteLine(" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                stopwatch.Restart();
                UShortSimdProcessor.HwAcceleratedSumUnchecked(ushortsOne, ushortsTwo, result);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"HW accelerated addition took: {hwTimeMs}ms (last value = {result[result.Length - 1]}).");
                
                stopwatch.Restart();
                UShortSimdProcessor.NaiveSumUnchecked(ushortsOne, ushortsTwo, result);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive addition took:          {naiveTimeMs}ms (last value = {result[result.Length - 1]}).");
            }

            WriteLine("Testing ushort array addition");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestUShortMinMaxFunctions(int testSetSize) {
            WriteLine();
            Write($"Testing ushort min/max functions, generating test data...");
            var testData = GetRandomUShortArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                ushort min;
                ushort max;

                stopwatch.Restart();
                UShortSimdProcessor.NaiveMinMax(testData, out min, out max);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimeMs}ms (min: {min}, max: {max}).");

                stopwatch.Restart();
                UShortSimdProcessor.HWAcceleratedMinMax(testData, out min, out max);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimeMs}ms (min: {min}, max: {max}).");
            }

            WriteLine("Finding min & max of ushorts");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestIntMaxMinFunctions(int testSetSize) {
            WriteLine();
            Write($"Testing int min/max functions, generating test data...");
            var testData = GetRandomIntArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                int min, max;
                stopwatch.Restart();
                IntSimdProcessor.NaiveMinMax(testData, out min, out max);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimesMs.Average():.##}ms (min: {min}, max: {max}).");

                stopwatch.Restart();
                IntSimdProcessor.HWAcceleratedMinMax(testData, out min, out max);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimesMs.Average():.##}ms (min: {min}, max: {max}).");
            }

            WriteLine("Finding min & max of ints");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average()}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average()}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestUshortArrayTotalFunctions(int testSetSize) {
            WriteLine();
            Write($"Testing ushort array total functions, generating test data...");
            var testData = GetRandomUShortArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                stopwatch.Restart();
                var total = UShortSimdProcessor.NaiveTotalOfArray(testData);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimeMs}ms (total: {total}).");

                stopwatch.Restart();
                total = UShortSimdProcessor.HWAcceleratedTotalOfArray(testData);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimeMs}ms (total: {total}).");
            }

            WriteLine("Finding total of array of ushorts");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestUshortArrayUncheckedTotalFunctions(int testSetSize) {
            WriteLine();
            Write($"Testing ushort array unchecked total functions, generating test data...");
            var testData = GetRandomUShortArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                stopwatch.Restart();
                var total = UShortSimdProcessor.NaiveUncheckedTotalOfArray(testData);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimeMs}ms (total: {total}).");

                stopwatch.Restart();
                total = UShortSimdProcessor.HWAcceleratedUncheckedTotalOfArray(testData);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimeMs}ms (total: {total}).");
            }

            WriteLine("Finding total of array of ushorts");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }

        public static void TestUshortArrayStats(int testSetSize) {
            WriteLine();
            Write($"Testing ushort array stats functions, generating test data...");
            var testData = GetRandomUShortArray(testSetSize);
            WriteLine($" done, testing...");

            var naiveTimesMs = new List<long>();
            var hwTimesMs = new List<long>();
            for (var i = 0; i < 3; i++) {
                stopwatch.Restart();
                ushort max, min;
                double average;
                UShortSimdProcessor.NaiveGetStats(testData, out min, out max, out average);
                var naiveTimeMs = stopwatch.ElapsedMilliseconds;
                naiveTimesMs.Add(naiveTimeMs);
                WriteLine($"Naive analysis took:                {naiveTimeMs}ms (min: {min}, max: {max}, average: {average}).");

                stopwatch.Restart();
                UShortSimdProcessor.HWAcceleratedGetStats(testData, out min, out max, out average);
                var hwTimeMs = stopwatch.ElapsedMilliseconds;
                hwTimesMs.Add(hwTimeMs);
                WriteLine($"Hareware accelerated analysis took: {hwTimeMs}ms (min: {min}, max: {max}, average: {average}).");
            }

            WriteLine("Finding stats of array of ushorts");
            WriteLine($"Naive method average time:          {naiveTimesMs.Average():.##}");
            WriteLine($"HW accelerated method average time: {hwTimesMs.Average():.##}");
            WriteLine($"Hardware speedup:                   {naiveTimesMs.Average() / hwTimesMs.Average():P}%");
        }
    }
}