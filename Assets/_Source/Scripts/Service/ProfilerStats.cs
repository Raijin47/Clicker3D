using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

namespace Pack.Debug.System
{
    public class ProfilerStats : MonoBehaviour
    {
        [SerializeField] private TMP_Text _profilerText;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _hidePosition;
        [SerializeField] private float _showPosition;

        private bool _isActive;
        private bool _isShow;

        private ProfilerRecorder _totalReservedMemoryRecorder;
        private ProfilerRecorder _gcReservedMemoryRecorder;
        private ProfilerRecorder _systemUsedMemoryRecorder;
        private ProfilerRecorder _setPassCallsRecorder;
        private ProfilerRecorder _drawCallsRecorder;
        private ProfilerRecorder _verticesRecorder;
        private ProfilerRecorder _trianglesRecorder;
        private ProfilerRecorder _mainThreadTimeRecorder;
        private ProfilerRecorder _renderRecorder;

        private readonly StringBuilder _sb = new(1000);

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public bool IsShow
        {
            set
            {
                _isShow = !_isShow;
                _rectTransform.anchoredPosition = new Vector2(_isShow ? _hidePosition : _showPosition, 300);
            }
        }


        private void OnEnable()
        {
            _isActive = true;

            _totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
            _gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
            _systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
            _setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            _drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            _verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
            _trianglesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
            _mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
            _renderRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Camera.Render");
        }

        private void OnDisable()
        {
            _isActive = false;

            _totalReservedMemoryRecorder.Dispose();
            _gcReservedMemoryRecorder.Dispose();
            _systemUsedMemoryRecorder.Dispose();
            _setPassCallsRecorder.Dispose();
            _drawCallsRecorder.Dispose();
            _verticesRecorder.Dispose();
            _trianglesRecorder.Dispose();
            _mainThreadTimeRecorder.Dispose();
            _renderRecorder.Dispose();
        }

        private void Start()
        {
            RunStats().Forget();
        }

        private async UniTask RunStats()
        {


            while (_isActive)
            {
                await UniTask.DelayFrame(10);
                if (_profilerText == null) continue;

                _sb.AppendLine($"<color=red>PROFILER RECORDER:</color>");

                if (_mainThreadTimeRecorder.Valid)
                {
                    _sb.AppendLine(
                        $"{GetName("CPU")} {GetRecorderFrameAverage(_mainThreadTimeRecorder) * 1e-6f:F1} ms");
                }

                if (_setPassCallsRecorder.Valid)
                {
                    _sb.AppendLine($"{GetName("SET PASS")} {_setPassCallsRecorder.LastValue}");
                }

                if (_drawCallsRecorder.Valid)
                {
                    _sb.AppendLine($"{GetName("DRAW CALLS")} {_drawCallsRecorder.LastValue}");
                }

                if (_verticesRecorder.Valid)
                {
                    _sb.AppendLine($"{GetName("VERTICES IN SCENE")} {_verticesRecorder.LastValue}");
                }

                if (_trianglesRecorder.Valid)
                {
                    _sb.AppendLine($"{GetName("TRIANGLES IN SCENE")} {_trianglesRecorder.LastValue}");
                }

                if (_totalReservedMemoryRecorder.Valid)
                {
                    _sb.AppendLine(
                        $"{GetName("TOTAL RESERVED MEMORY")} {SizeUtilities.SizeSuffix(_totalReservedMemoryRecorder.LastValue)}");
                }

                if (_gcReservedMemoryRecorder.Valid)
                {
                    _sb.AppendLine(
                        $"{GetName("GC RESERVED MEMORY")} {SizeUtilities.SizeSuffix(_gcReservedMemoryRecorder.LastValue)}");
                }

                if (_systemUsedMemoryRecorder.Valid)
                {
                    _sb.AppendLine(
                        $"{GetName("SYSTEM USED MEMORY")} {SizeUtilities.SizeSuffix(_systemUsedMemoryRecorder.LastValue)}");
                }

                _sb.AppendLine($"<color=red>\nPROFILER:</color>");
                _sb.AppendLine(
                    $"{GetName("ALLOCATED RAM")} {SizeUtilities.SizeSuffix(Profiler.GetTotalAllocatedMemoryLong())}");
                _sb.AppendLine(
                    $"{GetName("RESERVED RAM")} {SizeUtilities.SizeSuffix(Profiler.GetTotalReservedMemoryLong())}");
                _sb.Append($"{GetName("MONO RAM")} {SizeUtilities.SizeSuffix(Profiler.GetMonoUsedSizeLong())}");

                _profilerText.text = _sb.ToString();
                _sb.Clear();
            }
        }

        private static double GetRecorderFrameAverage(ProfilerRecorder recorder)
        {
            var samplesCount = recorder.Capacity;

            if (samplesCount == 0)
            {
                return 0;
            }

            var samples = new List<ProfilerRecorderSample>(samplesCount);
            recorder.CopyTo(samples);
            var r = samples.Aggregate<ProfilerRecorderSample, double>(0, (current, t) => current + t.Value);
            r /= samplesCount;

            return r;
        }

        private static string GetName(string text)
        {
            return $"<color=orange>{text}:</color>";
        }
    }
}