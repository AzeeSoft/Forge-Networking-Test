using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0]")]
	public partial class HoverCraftNetworkObject : NetworkObject
	{
		public const int IDENTITY = 8;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private int _lapsCompleted;
		public event FieldEvent<int> lapsCompletedChanged;
		public Interpolated<int> lapsCompletedInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int lapsCompleted
		{
			get { return _lapsCompleted; }
			set
			{
				// Don't do anything if the value is the same
				if (_lapsCompleted == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_lapsCompleted = value;
				hasDirtyFields = true;
			}
		}

		public void SetlapsCompletedDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_lapsCompleted(ulong timestep)
		{
			if (lapsCompletedChanged != null) lapsCompletedChanged(_lapsCompleted, timestep);
			if (fieldAltered != null) fieldAltered("lapsCompleted", _lapsCompleted, timestep);
		}
		[ForgeGeneratedField]
		private int _racePosition;
		public event FieldEvent<int> racePositionChanged;
		public Interpolated<int> racePositionInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int racePosition
		{
			get { return _racePosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_racePosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_racePosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetracePositionDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_racePosition(ulong timestep)
		{
			if (racePositionChanged != null) racePositionChanged(_racePosition, timestep);
			if (fieldAltered != null) fieldAltered("racePosition", _racePosition, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			lapsCompletedInterpolation.current = lapsCompletedInterpolation.target;
			racePositionInterpolation.current = racePositionInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _lapsCompleted);
			UnityObjectMapper.Instance.MapBytes(data, _racePosition);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_lapsCompleted = UnityObjectMapper.Instance.Map<int>(payload);
			lapsCompletedInterpolation.current = _lapsCompleted;
			lapsCompletedInterpolation.target = _lapsCompleted;
			RunChange_lapsCompleted(timestep);
			_racePosition = UnityObjectMapper.Instance.Map<int>(payload);
			racePositionInterpolation.current = _racePosition;
			racePositionInterpolation.target = _racePosition;
			RunChange_racePosition(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _lapsCompleted);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _racePosition);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (lapsCompletedInterpolation.Enabled)
				{
					lapsCompletedInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					lapsCompletedInterpolation.Timestep = timestep;
				}
				else
				{
					_lapsCompleted = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_lapsCompleted(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (racePositionInterpolation.Enabled)
				{
					racePositionInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					racePositionInterpolation.Timestep = timestep;
				}
				else
				{
					_racePosition = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_racePosition(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (lapsCompletedInterpolation.Enabled && !lapsCompletedInterpolation.current.UnityNear(lapsCompletedInterpolation.target, 0.0015f))
			{
				_lapsCompleted = (int)lapsCompletedInterpolation.Interpolate();
				//RunChange_lapsCompleted(lapsCompletedInterpolation.Timestep);
			}
			if (racePositionInterpolation.Enabled && !racePositionInterpolation.current.UnityNear(racePositionInterpolation.target, 0.0015f))
			{
				_racePosition = (int)racePositionInterpolation.Interpolate();
				//RunChange_racePosition(racePositionInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public HoverCraftNetworkObject() : base() { Initialize(); }
		public HoverCraftNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public HoverCraftNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
