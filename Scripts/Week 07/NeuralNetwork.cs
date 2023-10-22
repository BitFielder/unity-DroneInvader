using System.Collections.Generic;
using UnityEngine;

namespace Meta.Scripts
{

    [System.Serializable]
    public class NeuralNetwork {
        public int[] layers;
        public float[][] neurons;
        public float[][][] weights;

        public NeuralNetwork (int[] layers) {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++) {
                this.layers[i] = layers[i];
            }

            InitNeurons();
            InitWeights();
        }

        private void InitNeurons() {
            List<float[]> neuronsList = new List<float[]>();

            for (int i = 0; i < layers.Length; i++) {
                neuronsList.Add(new float[layers[i]]);
            }

            neurons = neuronsList.ToArray();
        }

        private void InitWeights() {
            List<float[][]> weightsList = new List<float[][]>();

            for (int i = 1; i < layers.Length; i++) {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j < neurons[i].Length; j++) {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++) {
                        neuronWeights[k] = Random.Range(-0.5f, 0.5f);
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }

            weights = weightsList.ToArray();
        }

        public float[] FeedForward(float[] inputs) {
            for (int i = 0; i < inputs.Length; i++) {
                neurons[0][i] = inputs[i];
            }

            for (int i = 1; i < layers.Length; i++) {
                for (int j = 0; j < neurons[i].Length; j++) {
                    float value = 0f;
                    for (int k = 0; k < neurons[i - 1].Length; k++) {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }

                    neurons[i][j] = (float)System.Math.Tanh(value);
                }
            }

            return neurons[^1];
        }
    }

}
