#if UNITY_2020_1_OR_NEWER
using UnityEngine;
using Unity.MLAgents.Sensors;

namespace Unity.MLAgents.Extensions.Sensors
{
    public class ArticulationBodySensorComponent  : SensorComponent
    {
        public ArticulationBody RootBody;

        [SerializeField]
        public PhysicsSensorSettings Settings = PhysicsSensorSettings.Default();
        public string sensorName;

        /// <summary>
        /// Creates a PhysicsBodySensor.
        /// </summary>
        /// <returns></returns>
        public override ISensor CreateSensor()
        {
            return new PhysicsBodySensor(RootBody, Settings, sensorName);
        }

        /// <inheritdoc/>
        public override int[] GetObservationShape()
        {
            if (RootBody == null)
            {
                return new[] { 0 };
            }

            // TODO static method in PhysicsBodySensor?
            // TODO only update PoseExtractor when body changes?
            var poseExtractor = new ArticulationBodyPoseExtractor(RootBody);
            var numTransformObservations = Settings.TransformSize(poseExtractor.NumPoses);
            return new[] { numTransformObservations };
        }
    }

}
#endif // UNITY_2020_1_OR_NEWER