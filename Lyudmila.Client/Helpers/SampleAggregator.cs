// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;

using NAudio.Dsp;

namespace Lyudmila.Client.Helpers
{
    public class SampleAggregator
    {
        private readonly int binaryExponentitation;
        private readonly int bufferSize;
        private readonly Complex[] channelData;
        private int channelDataPosition;

        public SampleAggregator(int bufferSize)
        {
            this.bufferSize = bufferSize;
            binaryExponentitation = (int) Math.Log(bufferSize, 2);
            channelData = new Complex[bufferSize];
        }

        public float LeftMaxVolume { get; private set; }

        public float LeftMinVolume { get; private set; }

        public float RightMaxVolume { get; private set; }

        public float RightMinVolume { get; private set; }

        public void Clear()
        {
            LeftMaxVolume = float.MinValue;
            RightMaxVolume = float.MinValue;
            LeftMinVolume = float.MaxValue;
            RightMinVolume = float.MaxValue;
            channelDataPosition = 0;
        }

        /// <summary>
        ///   Add a sample value to the aggregator.
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="rightValue"></param>
        public void Add(float leftValue, float rightValue)
        {
            if(channelDataPosition == 0)
            {
                LeftMaxVolume = float.MinValue;
                RightMaxVolume = float.MinValue;
                LeftMinVolume = float.MaxValue;
                RightMinVolume = float.MaxValue;
            }

            // Make stored channel data stereo by averaging left and right values.
            channelData[channelDataPosition].X = (leftValue + rightValue) / 2.0f;
            channelData[channelDataPosition].Y = 0;
            channelDataPosition++;

            LeftMaxVolume = Math.Max(LeftMaxVolume, leftValue);
            LeftMinVolume = Math.Min(LeftMinVolume, leftValue);
            RightMaxVolume = Math.Max(RightMaxVolume, rightValue);
            RightMinVolume = Math.Min(RightMinVolume, rightValue);

            if(channelDataPosition >= channelData.Length)
            {
                channelDataPosition = 0;
            }
        }

        /// <summary>
        ///   Performs an FFT calculation on the channel data upon request.
        /// </summary>
        /// <param name="fftBuffer">A buffer where the FFT data will be stored.</param>
        public void GetFFTResults(float[] fftBuffer)
        {
            var channelDataClone = new Complex[bufferSize];
            channelData.CopyTo(channelDataClone, 0);
            FastFourierTransform.FFT(true, binaryExponentitation, channelDataClone);
            for(var i = 0; i < channelDataClone.Length / 2; i++)
            {
                // Calculate actual intensities for the FFT results.
                fftBuffer[i] = (float) Math.Sqrt(channelDataClone[i].X * channelDataClone[i].X + channelDataClone[i].Y * channelDataClone[i].Y);
            }
        }
    }
}