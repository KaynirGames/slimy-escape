using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioMaster))]
public class AudioMasterEditor : Editor
{
    private AudioMaster audioMaster;

    private void OnEnable()
    {
        audioMaster = (AudioMaster)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (audioMaster.soundEffects.Count == 0)
        {
            EditorGUILayout.LabelField("Список звуковых эффектов пуст.");
            return;
        }

        EditorGUILayout.LabelField("Настройка и предпросмотр звуковых эффектов:");

        foreach (var sound in audioMaster.soundEffects)
        {
            EditorGUILayout.BeginVertical("box");

            if (sound != null)
            {
                sound.name = EditorGUILayout.TextField("Название звукового эффекта", sound.soundName);
                sound.clip = (AudioClip)EditorGUILayout.ObjectField("Аудиодорожка", sound.clip, typeof(AudioClip), false);
                sound.volume = EditorGUILayout.Slider("Громкость воспроизведения", sound.volume, 0f, 1f);
                sound.pitch = EditorGUILayout.Slider("Высота звука", sound.pitch, .1f, 3f);
                sound.loop = EditorGUILayout.Toggle("Воспроизведение в цикле", sound.loop);

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Прослушать"))
                {
                    audioMaster.PreviewSound(sound.name);
                }

                if (GUILayout.Button("Остановить"))
                {
                    audioMaster.previewSource.Stop();
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField($"Элемент с индексом {audioMaster.soundEffects.IndexOf(sound)} пуст.");
            }

            EditorGUILayout.EndVertical();
        }
    }
}
