using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Tilemaps;

namespace UnityEngine
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Rule Override Tile", menuName = "Tiles/Rule Override Tile")]
    public class RuleOverrideTile : TileBase
    {
        public bool m_Advanced;
        public OverrideTilingRule m_OverrideDefault = new OverrideTilingRule();
        public List<OverrideTilingRule> m_OverrideTilingRules = new List<OverrideTilingRule>();

        private RuleTile m_RuntimeTile;
        public List<TileSpritePair> m_Sprites = new List<TileSpritePair>();

        public RuleTile m_Tile;

        public Sprite this[Sprite originalSprite]
        {
            get
            {
                foreach (var spritePair in m_Sprites)
                {
                    if (spritePair.m_OriginalSprite == originalSprite)
                    {
                        return spritePair.m_OverrideSprite;
                    }
                }

                return null;
            }
            set
            {
                if (value == null)
                {
                    m_Sprites = m_Sprites.Where(spritePair => spritePair.m_OriginalSprite != originalSprite).ToList();
                }
                else
                {
                    foreach (var spritePair in m_Sprites)
                    {
                        if (spritePair.m_OriginalSprite == originalSprite)
                        {
                            spritePair.m_OverrideSprite = value;
                            return;
                        }
                    }

                    m_Sprites.Add(new TileSpritePair
                    {
                        m_OriginalSprite = originalSprite,
                        m_OverrideSprite = value
                    });
                }
            }
        }

        public RuleTile.TilingRule this[RuleTile.TilingRule originalRule]
        {
            get
            {
                if (!m_Tile)
                    return null;

                var index = m_Tile.m_TilingRules.IndexOf(originalRule);
                if (index == -1)
                    return null;
                if (m_OverrideTilingRules.Count < index + 1)
                    return null;

                return m_OverrideTilingRules[index].m_Enabled ? m_OverrideTilingRules[index].m_TilingRule : null;
            }
            set
            {
                if (!m_Tile)
                    return;

                var index = m_Tile.m_TilingRules.IndexOf(originalRule);
                if (index == -1)
                    return;

                if (value == null)
                {
                    if (m_OverrideTilingRules.Count < index + 1)
                        return;
                    m_OverrideTilingRules[index].m_Enabled = false;
                    while (m_OverrideTilingRules.Count > 0 && !m_OverrideTilingRules[m_OverrideTilingRules.Count - 1].m_Enabled)
                        m_OverrideTilingRules.RemoveAt(m_OverrideTilingRules.Count - 1);
                }
                else
                {
                    while (m_OverrideTilingRules.Count < index + 1)
                        m_OverrideTilingRules.Add(new OverrideTilingRule());
                    m_OverrideTilingRules[index].m_Enabled = true;
                    m_OverrideTilingRules[index].m_TilingRule = CloneTilingRule(value);
                    m_OverrideTilingRules[index].m_TilingRule.m_Neighbors = null;
                }
            }
        }

        public RuleTile.TilingRule m_OriginalDefault
        {
            get
            {
                return new RuleTile.TilingRule
                {
                    m_Sprites = new[] {m_Tile != null ? m_Tile.m_DefaultSprite : null},
                    m_ColliderType = m_Tile != null ? m_Tile.m_DefaultColliderType : Tile.ColliderType.None
                };
            }
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            if (!m_RuntimeTile)
                Override();
            return m_RuntimeTile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
        }

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            if (!m_RuntimeTile)
                Override();
            m_RuntimeTile.GetTileData(position, tilemap, ref tileData);
        }

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            if (!m_RuntimeTile)
                Override();
            m_RuntimeTile.RefreshTile(position, tilemap);
        }

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            Override();
            return m_RuntimeTile.StartUp(position, tilemap, go);
        }

        public void ApplyOverrides(IList<KeyValuePair<Sprite, Sprite>> overrides)
        {
            if (overrides == null)
                throw new ArgumentNullException("overrides");

            for (var i = 0; i < overrides.Count; i++)
                this[overrides[i].Key] = overrides[i].Value;
        }

        public void GetOverrides(List<KeyValuePair<Sprite, Sprite>> overrides)
        {
            if (overrides == null)
                throw new ArgumentNullException("overrides");

            overrides.Clear();

            if (!m_Tile)
                return;

            var originalSprites = new List<Sprite>();

            if (m_Tile.m_DefaultSprite)
                originalSprites.Add(m_Tile.m_DefaultSprite);

            foreach (var rule in m_Tile.m_TilingRules)
            foreach (var sprite in rule.m_Sprites)
                if (sprite && !originalSprites.Contains(sprite))
                    originalSprites.Add(sprite);

            foreach (var sprite in originalSprites)
                overrides.Add(new KeyValuePair<Sprite, Sprite>(sprite, this[sprite]));
        }

        public void ApplyOverrides(IList<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRule>> overrides)
        {
            if (overrides == null)
                throw new ArgumentNullException("overrides");

            for (var i = 0; i < overrides.Count; i++)
                this[overrides[i].Key] = overrides[i].Value;
        }

        public void GetOverrides(List<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRule>> overrides)
        {
            if (overrides == null)
                throw new ArgumentNullException("overrides");

            overrides.Clear();

            if (!m_Tile)
                return;

            foreach (var originalRule in m_Tile.m_TilingRules)
            {
                var overrideRule = this[originalRule];
                overrides.Add(new KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRule>(originalRule, overrideRule));
            }

            overrides.Add(new KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRule>(m_OriginalDefault, m_OverrideDefault.m_TilingRule));
        }

        public void Override()
        {
            m_RuntimeTile = m_Tile ? Instantiate(m_Tile) : new RuleTile();
            m_RuntimeTile.m_Self = this;
            if (!m_Advanced)
            {
                if (m_RuntimeTile.m_DefaultSprite)
                    m_RuntimeTile.m_DefaultSprite = this[m_RuntimeTile.m_DefaultSprite];
                if (m_RuntimeTile.m_TilingRules != null)
                    foreach (var rule in m_RuntimeTile.m_TilingRules)
                        for (var i = 0; i < rule.m_Sprites.Length; i++)
                            if (rule.m_Sprites[i])
                                rule.m_Sprites[i] = this[rule.m_Sprites[i]];
            }
            else
            {
                if (m_OverrideDefault.m_Enabled)
                {
                    m_RuntimeTile.m_DefaultSprite = m_OverrideDefault.m_TilingRule.m_Sprites.Length > 0 ? m_OverrideDefault.m_TilingRule.m_Sprites[0] : null;
                    m_RuntimeTile.m_DefaultColliderType = m_OverrideDefault.m_TilingRule.m_ColliderType;
                }

                if (m_RuntimeTile.m_TilingRules != null)
                {
                    for (var i = 0; i < m_RuntimeTile.m_TilingRules.Count; i++)
                    {
                        var originalRule = m_RuntimeTile.m_TilingRules[i];
                        var overrideRule = this[m_Tile.m_TilingRules[i]];
                        if (overrideRule == null)
                            continue;
                        CopyTilingRule(overrideRule, originalRule, false);
                    }
                }
            }
        }

        public RuleTile.TilingRule CloneTilingRule(RuleTile.TilingRule from)
        {
            var clone = new RuleTile.TilingRule();
            CopyTilingRule(from, clone, true);
            return clone;
        }

        public void CopyTilingRule(RuleTile.TilingRule from, RuleTile.TilingRule to, bool copyRule)
        {
            if (copyRule)
            {
                to.m_Neighbors = from.m_Neighbors;
                to.m_RuleTransform = from.m_RuleTransform;
            }

            to.m_Sprites = from.m_Sprites.Clone() as Sprite[];
            to.m_AnimationSpeed = from.m_AnimationSpeed;
            to.m_PerlinScale = from.m_PerlinScale;
            to.m_Output = from.m_Output;
            to.m_ColliderType = from.m_ColliderType;
            to.m_RandomTransform = from.m_RandomTransform;
        }

        [Serializable]
        public class TileSpritePair
        {
            public Sprite m_OriginalSprite;
            public Sprite m_OverrideSprite;
        }

        [Serializable]
        public class OverrideTilingRule
        {
            public bool m_Enabled;
            public RuleTile.TilingRule m_TilingRule = new RuleTile.TilingRule();
        }
    }
}