using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Validation;
using Sirenix.Utilities.Editor;
using UnityEngine;

[DrawerPriority(0.0, 10000.1)]
public class MiniValidationDrawer<T> : OdinValueDrawer<T>, IDisposable
{
	private List<ValidationResult> _validationResults;
	private bool                   _rerunFullValidation;
	private object                 _shakeGroupKey;
	private ValidationComponent    _validationComponent;

	private static readonly Color BG     = new Color(1, 0, 0, 0.15f);
	private static readonly Color Shadow = new Color(0, 0, 0, 0.3f);
	private static readonly Color Band   = new Color(1, 0, 0, 0.5f);

	private static readonly Color WarnBG     = new Color(0.79f, 0.52f, 0f, 0.15f);
	private static readonly Color WarnShadow = new Color(0, 0, 0, .3f);
	private static readonly Color WarnBand   = new Color(1f, 0.62f, 0.22f, 0.5f);

	protected override bool CanDrawValueProperty(InspectorProperty property)
	{
		ValidationComponent component = property.GetComponent<ValidationComponent>();
		return component != null && component.ValidatorLocator.PotentiallyHasValidatorsFor(property);
	}

	protected override void Initialize()
	{
		_validationComponent = Property.GetComponent<ValidationComponent>();
		_validationComponent.ValidateProperty(ref _validationResults);
		if (_validationResults.Count > 0)
		{
			_shakeGroupKey                    =  UniqueDrawerKey.Create(Property, this);
			Property.Tree.OnUndoRedoPerformed += OnUndoRedoPerformed;
			ValueEntry.OnValueChanged         += OnValueChanged;
			ValueEntry.OnChildValueChanged    += OnChildValueChanged;
		}
		else
		{
			SkipWhenDrawing = true;
		}
	}

	protected override void DrawPropertyLayout(GUIContent label)
	{
		if (_validationResults.Count == 0)
		{
			CallNextDrawer(label);
		}
		else
		{
			GUILayout.BeginVertical();
			SirenixEditorGUI.BeginShakeableGroup(_shakeGroupKey);

			ValidationResult error   = null;
			ValidationResult warning = null;

			for (var i = 0; i < _validationResults.Count; ++i)
			{
				ValidationResult result = _validationResults[i];

				if (Event.current.type == EventType.Layout && (_rerunFullValidation || result.Setup.Validator.RevalidationCriteria == RevalidationCriteria.Always))
				{
					ValidationResultType resultType = result.ResultType;

					result.Setup.ParentInstance = Property.ParentValues[0];
					result.Setup.Value          = ValueEntry.Values[0];
					result.RerunValidation();

					if (resultType != result.ResultType && result.ResultType != ValidationResultType.Valid)
						SirenixEditorGUI.StartShakingGroup(_shakeGroupKey);
				}

				switch (result.ResultType)
				{
					case ValidationResultType.Error:
						error = result;
						break;

					case ValidationResultType.Warning:
						warning = result;
						break;

					case ValidationResultType.Valid when !string.IsNullOrEmpty(result.Message):
						SirenixEditorGUI.InfoMessageBox(result.Message);
						break;
				}
			}

			if (Event.current.type == EventType.Layout)
				_rerunFullValidation = false;

			// The hack materializes: we skip the next drawer which should be the default ValidationDrawer
			Property.GetActiveDrawerChain().MoveNext();
			CallNextDrawer(label);

			if (error != null)
			{
				if (label != null)
					label.tooltip = $"ERROR: {error.Message}";

				Rect rect = GUIHelper.GetCurrentLayoutRect();
				if (Event.current.type == EventType.Repaint)
				{
					SirenixEditorGUI.DrawSolidRect(rect, BG);
					SirenixEditorGUI.DrawBorders(rect, 0, 0, 1, 0, Shadow);
					SirenixEditorGUI.DrawBorders(rect, 3, 0, 0, 0, BG);
				}
			}
			else if (warning != null)
			{
				label.tooltip = $"WARNING: {warning.Message}";

				Rect rect = GUIHelper.GetCurrentLayoutRect();
				if (Event.current.type == EventType.Repaint)
				{
					SirenixEditorGUI.DrawSolidRect(rect, WarnBG);
					SirenixEditorGUI.DrawBorders(rect, 0, 0, 1, 0, WarnShadow);
					SirenixEditorGUI.DrawBorders(rect, 3, 0, 0, 0, WarnBand);
				}
			}

			SirenixEditorGUI.EndShakeableGroup(_shakeGroupKey);
			GUILayout.EndVertical();
		}
	}

	public void Dispose()
	{
		if (_validationResults.Count > 0)
		{
			Property.Tree.OnUndoRedoPerformed -= OnUndoRedoPerformed;
			ValueEntry.OnValueChanged         -= OnValueChanged;
			ValueEntry.OnChildValueChanged    -= OnChildValueChanged;
		}

		_validationResults = null;
	}

	private void OnUndoRedoPerformed()
	{
		_rerunFullValidation = true;
	}

	private void OnValueChanged(int index)
	{
		_rerunFullValidation = true;
	}

	private void OnChildValueChanged(int index)
	{
		_rerunFullValidation = true;
	}
}
