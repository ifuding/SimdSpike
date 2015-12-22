﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static SimdSpike.Utilities;

namespace SimdSpike {
    public static class MethodValidation {
        public static void ValidateFloatAdditionMethods(params Action<float[], float[], float[]>[] sumActions) {
            ValidateFloatAdditionMethods((IEnumerable<Action<float[], float[], float[]>>)sumActions);
        }

        public static void ValidateFloatAdditionInPlaceMethods(params Action<float[], float[]>[] additionInPlaceActions) {
            ValidateFloatAdditionInPlaceMethods((IEnumerable<Action<float[], float[]>>)additionInPlaceActions);
        }
        public static void ValidateFloatAdditionFuncs(params Func<float[], float[], float[]>[] sumActions) {
            ValidateFloatAdditionFuncs((IEnumerable<Func<float[], float[], float[]>>)sumActions);
        }

        private static void ValidateFloatAdditionMethods(IEnumerable<Action<float[], float[], float[]>> sumActions) {
            foreach (var sumAction in sumActions) {
                ValidateAdditionMethod(sumAction);
            }
        }

        private static void ValidateAdditionMethod(Action<float[], float[], float[]> sumAction) {
            const int count = 23;
            var one = Enumerable.Range(0, count).Select(x => RandomFloat()).ToArray();
            var two = Enumerable.Range(0, count).Select(x => RandomFloat()).ToArray();
            var sum = new float[one.Length];
            sumAction(one, two, sum);

            for (var i = 0; i < one.Length; i++) {
                Assert.AreEqual(one[i] + two[i], sum[i]);
            }
        }

        private static void ValidateFloatAdditionInPlaceMethods(IEnumerable<Action<float[], float[]>> additionInPlaceActions) {
            additionInPlaceActions.Select(x => {
                return new Action<float[], float[], float[]>((lhs, rhs, result) => {
                    lhs.CopyTo(result, 0);
                    x(result, rhs);
                });
            }).ToList().ForEach(ValidateAdditionMethod);
        }

        private static void ValidateFloatAdditionFuncs(IEnumerable<Func<float[], float[], float[]>> additionFuncs) {
            additionFuncs.Select(x => {
                return new Action<float[], float[], float[]>((lhs, rhs, result) => {
                    var temp = x(lhs, rhs);
                    temp.CopyTo(result, 0);
                });
            }).ToList().ForEach(ValidateAdditionMethod);
        }
    }
}