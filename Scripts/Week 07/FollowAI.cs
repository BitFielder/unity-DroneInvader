using UnityEngine;

namespace Meta.Scripts
{
    public class FollowAI : MonoBehaviour
    {
        public float speed = 5f;
        
        private NeuralNetwork network;
        private Transform playerTransform;

        private void Start()
        {
            // Initialize the network with 2 inputs (for the player's relative position),
            // a hidden layer with 5 neurons, and 2 outputs (for the enemy's movement direction)
            network = new NeuralNetwork(new int[] { 2, 5, 2 });

            // Assume there's a GameObject tagged "Player" that the enemy is following
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            Vector3 relativePosition = playerTransform.position - transform.position;

            // Use the neural network to determine the direction to move
            float[] output = network.FeedForward(new float[] { relativePosition.x, relativePosition.z });

            // Move the enemy
            Vector3 direction = new Vector3(output[0], 0, output[1]).normalized;
            transform.position += direction * (speed * Time.deltaTime);
        }
    }

}