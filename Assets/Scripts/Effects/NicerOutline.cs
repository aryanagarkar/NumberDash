using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Credit Melang, Lee Hui (Unity UI Extensions)
/// Local copy kept for slot hover outline — the full UI Extensions package is not used.
namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Effects/Nicer Outline")]
	public class NicerOutline : BaseMeshEffect
	{
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		[SerializeField]
		private bool m_UseGraphicAlpha = true;

		private readonly List<UIVertex> m_Verts = new List<UIVertex>();

		public Color effectColor
		{
			get { return m_EffectColor; }
			set
			{
				m_EffectColor = value;
				if (graphic != null)
					graphic.SetVerticesDirty();
			}
		}

		public Vector2 effectDistance
		{
			get { return m_EffectDistance; }
			set
			{
				value.x = Mathf.Clamp(value.x, -600f, 600f);
				value.y = Mathf.Clamp(value.y, -600f, 600f);
				if (m_EffectDistance == value)
					return;
				m_EffectDistance = value;
				if (graphic != null)
					graphic.SetVerticesDirty();
			}
		}

		public bool useGraphicAlpha
		{
			get { return m_UseGraphicAlpha; }
			set
			{
				m_UseGraphicAlpha = value;
				if (graphic != null)
					graphic.SetVerticesDirty();
			}
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!IsActive())
				return;

			m_Verts.Clear();
			vh.GetUIVertexStream(m_Verts);

			float distanceX = effectDistance.x;
			float distanceY = effectDistance.y;

			vh.Clear();

			int start = 0;
			start += ApplyOutline(m_Verts, effectColor, distanceX, distanceY, vh, start);
			start += ApplyOutline(m_Verts, effectColor, distanceX, -distanceY, vh, start);
			start += ApplyOutline(m_Verts, effectColor, -distanceX, distanceY, vh, start);
			start += ApplyOutline(m_Verts, effectColor, -distanceX, -distanceY, vh, start);
			start += ApplyOutline(m_Verts, effectColor, distanceX, 0, vh, start);
			start += ApplyOutline(m_Verts, effectColor, -distanceX, 0, vh, start);
			start += ApplyOutline(m_Verts, effectColor, 0, distanceY, vh, start);
			start += ApplyOutline(m_Verts, effectColor, 0, -distanceY, vh, start);
			ApplyOriginal(m_Verts, vh, start);
		}

		private int ApplyOutline(List<UIVertex> verts, Color32 color, float x, float y, VertexHelper vh, int startIndex)
		{
			int length = verts.Count;
			for (int i = 0; i < length; ++i)
			{
				UIVertex vt = verts[i];
				Vector3 v = vt.position;
				v.x += x;
				v.y += y;
				vt.position = v;
				var newColor = color;
				if (m_UseGraphicAlpha)
					newColor.a = (byte)((newColor.a * verts[i].color.a) / 255);
				vt.color = newColor;
				vh.AddVert(vt);
			}

			int triangleCount = length / 3;
			for (int i = 0; i < triangleCount; ++i)
			{
				int start = startIndex + 3 * i;
				vh.AddTriangle(start, start + 1, start + 2);
			}

			return length;
		}

		private static int ApplyOriginal(List<UIVertex> verts, VertexHelper vh, int startIndex)
		{
			int length = verts.Count;
			for (int i = 0; i < length; ++i)
				vh.AddVert(verts[i]);

			int triangleCount = length / 3;
			for (int i = 0; i < triangleCount; ++i)
			{
				int start = startIndex + 3 * i;
				vh.AddTriangle(start, start + 1, start + 2);
			}

			return length;
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			effectDistance = m_EffectDistance;
			base.OnValidate();
		}
#endif
	}
}
