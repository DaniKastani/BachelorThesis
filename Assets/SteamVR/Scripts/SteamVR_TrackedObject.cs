using System.Text;
using UnityEngine;
using Valve.VR;

namespace Valve.VR
{
    public class SteamVR_TrackedObject : MonoBehaviour
    {
        public enum EIndex
        {
            None = -1,
            Hmd = (int)OpenVR.k_unTrackedDeviceIndex_Hmd,
            Device1,
            Device2,
            Device3,
            Device4,
            Device5,
            Device6,
            Device7,
            Device8,
            Device9,
            Device10,
            Device11,
            Device12,
            Device13,
            Device14,
            Device15
        }

        public EIndex index;

        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public bool isValid { get; private set; }

        public string trackerUID;
        int lastIndex = -1;

        void printAllUIDs()
        {
            for (uint x = 0; x < 15; x++)
            {
                ETrackedPropertyError error = new ETrackedPropertyError();
                StringBuilder sb = new StringBuilder();
                OpenVR.System.GetStringTrackedDeviceProperty(x, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
                var probablyUniqueDeviceSerial = sb.ToString();
                Debug.Log(x + ": " + probablyUniqueDeviceSerial);
            }
        }

        int getDeviceIndex(string UID)
        {

            for (uint x = 0; x < 15; x++)
            {
                ETrackedPropertyError error = new ETrackedPropertyError();
                StringBuilder sb = new StringBuilder();
                OpenVR.System.GetStringTrackedDeviceProperty(x, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
                var probablyUniqueDeviceSerial = sb.ToString();
                if (probablyUniqueDeviceSerial.Equals(UID))
                {
                    return (int)x;
                }
            }
            return -1;
        }

        private string getUID(uint deviceIndex)
        {
            ETrackedPropertyError error = new ETrackedPropertyError();
            StringBuilder sb = new StringBuilder();
            OpenVR.System.GetStringTrackedDeviceProperty(deviceIndex, ETrackedDeviceProperty.Prop_SerialNumber_String, sb, OpenVR.k_unMaxPropertyStringSize, ref error);
            return sb.ToString();
        }

        private void OnNewPoses(TrackedDevicePose_t[] poses)
        {

            if (trackerUID != null && trackerUID.Length > 2)
            {
                if (!getUID((uint)lastIndex).Equals(trackerUID))
                {
                    int currentIndex = getDeviceIndex(trackerUID);
                    SetDeviceIndex(currentIndex);
                    lastIndex = currentIndex;
                }
            }


            if (index == EIndex.None)
                return;

            var i = (int)index;

            isValid = false;
            if (poses.Length <= i)
                return;

            if (!poses[i].bDeviceIsConnected)
                return;

            if (!poses[i].bPoseIsValid)
                return;

            isValid = true;

            var pose = new SteamVR_Utils.RigidTransform(poses[i].mDeviceToAbsoluteTracking);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pose.pos);
                transform.rotation = origin.rotation * pose.rot;
            }
            else
            {
                transform.localPosition = pose.pos;
                transform.localRotation = pose.rot;
            }
        }

        SteamVR_Events.Action newPosesAction;

        SteamVR_TrackedObject()
        {
            newPosesAction = SteamVR_Events.NewPosesAction(OnNewPoses);
        }

        private void Awake()
        {
            OnEnable();
        }

        void OnEnable()
        {
            var render = SteamVR_Render.instance;
            if (render == null)
            {
                enabled = false;
                return;
            }

            newPosesAction.enabled = true;
        }

        void OnDisable()
        {
            newPosesAction.enabled = false;
            isValid = false;
        }

        public void SetDeviceIndex(int index)
        {
            if (System.Enum.IsDefined(typeof(EIndex), index))
                this.index = (EIndex)index;
        }
    }
}

