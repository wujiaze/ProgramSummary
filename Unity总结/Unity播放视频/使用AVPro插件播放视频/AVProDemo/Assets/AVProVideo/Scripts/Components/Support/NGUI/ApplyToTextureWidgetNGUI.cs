//#define NGUI
using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------
// Copyright 2015-2017 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

#if NGUI
namespace RenderHeads.Media.AVProVideo
{
	[AddComponentMenu("AVPro Video/Display NGUI")]
	public class ApplyToTextureWidgetNGUI : MonoBehaviour 
	{
		#region Fields
		[SerializeField]
		private UITexture _uiTexture = null;

		[SerializeField]
		private MediaPlayer MediaPlayer = null;

		public MediaPlayer Player
		{
			get { return MediaPlayer; }
			set { if (MediaPlayer != value) { ChangeMediaPlayer(value); _isDirty = true; } }
		}

		[SerializeField]
		private Texture2D _defaultTexture;

		public Texture2D DefaultTexture
		{
			get { return _defaultTexture; }
			set { if (_defaultTexture != value) { _defaultTexture = value; _isDirty = true; } }
		}

		[SerializeField]
		private bool _makePixelPerfect = false;

		public bool MakePixelPerfect
		{
			get { return _makePixelPerfect; }
			set { if (_makePixelPerfect != value) { _makePixelPerfect = value; _isDirty = true; } }
		}

		private bool _isDirty;
		private Texture _lastTextureApplied;
		#endregion

		private void TryUpdateTexture()
		{
			bool applied = false;

			// Try to apply texture from media
			if (MediaPlayer != null && MediaPlayer.TextureProducer != null)
			{
				Texture texture = MediaPlayer.TextureProducer.GetTexture();
				if (texture != null)
				{
					// Check for changing texture
					if (texture != _lastTextureApplied)
					{
						_isDirty = true;
					}

					if (_isDirty)
					{
						Apply(texture, MediaPlayer.TextureProducer.RequiresVerticalFlip());
					}
					applied = true;
				}
			}

			// If the media didn't apply a texture, then try to apply the default texture
			if (!applied)
			{
				if (_defaultTexture != _lastTextureApplied)
				{
					_isDirty = true;
				}
				if (_isDirty)
				{
					Apply(_defaultTexture, false);
				}
			}
		}

		private void Apply(Texture texture, bool requiresYFlip)
		{
			if (_uiTexture != null)
			{
				_isDirty = false;
				if (requiresYFlip)
				{
					_uiTexture.flip = UITexture.Flip.Vertically;
				}
				else
				{
					_uiTexture.flip = UITexture.Flip.Nothing;
				}

				_lastTextureApplied = _uiTexture.mainTexture = texture;

				if (_makePixelPerfect)
				{
					_uiTexture.MakePixelPerfect();
				}
			}
		}

		private void ChangeMediaPlayer(MediaPlayer newPlayer)
		{
			// When changing the media player, handle event subscriptions
			if (MediaPlayer != null)
			{
				MediaPlayer.Events.RemoveListener(OnMediaPlayerEvent);
				MediaPlayer = null;
			}

			MediaPlayer = newPlayer;
			if (MediaPlayer != null)
			{
				MediaPlayer.Events.AddListener(OnMediaPlayerEvent);
			}
		}

		// Callback function to handle events
		private void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
		{
			switch (et)
			{
				case MediaPlayerEvent.EventType.Closing:
					Apply(_defaultTexture, false);
					break;
				case MediaPlayerEvent.EventType.Started:
				case MediaPlayerEvent.EventType.FirstFrameReady:
					TryUpdateTexture();
					break;
			}
		}

		void Start()
		{
			if (_defaultTexture == null)
			{
				_defaultTexture = Texture2D.blackTexture;
			}
			ChangeMediaPlayer(MediaPlayer);
		}

		void Update()
		{
			TryUpdateTexture();
		}

		// We do a LateUpdate() to allow for any changes in the texture that may have happened in Update()
		void LateUpdate()
		{
			TryUpdateTexture();
		}

		void OnEnable()
		{
			TryUpdateTexture();
		}

		void OnDisable()
		{
			Apply(_defaultTexture, false);
		}

		void OnDestroy()
		{
			ChangeMediaPlayer(null);
		}
	}
}
#endif