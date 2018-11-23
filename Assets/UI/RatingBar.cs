using UnityEngine;
using UnityEngine.UI;

namespace UI {
    /// <inheritdoc />
    /// <summary>
    /// 显示评分组件。目前仅支持展示，不支持交互。
    /// </summary>
    [AddComponentMenu("UI/RatingBar", 100)]
    public class RatingBar : MonoBehaviour {
        /// <summary>
        /// 默认显示的图片。
        /// </summary>
        public Sprite NormalImage;

        /// <summary>
        /// 评分显示的图片。
        /// </summary>
        public Sprite RatingImage;

        /// <summary>
        /// 图片的宽度（默认为 12 px）。
        /// </summary>
        public int Width = 12;

        /// <summary>
        /// 图片的高度（默认为 12 px）。
        /// </summary>
        public int Height = 12;

        /// <summary>
        /// 要显示的评分数量（默认为 5）。
        /// </summary>
        public int NumStars = 5;

        /// <summary>
        /// 评分。
        /// </summary>
        public int Rating;

        private HorizontalLayoutGroup group;

        private void Start() {
            group = GetComponent<HorizontalLayoutGroup>();
            Invalidate();
        }

        private void Invalidate() {
            if (NormalImage == null || RatingImage == null || group == null)
                return;

            foreach (Transform child in group.transform) {
                Destroy(child.gameObject);
            }

            for (var num = 1; num <= NumStars; num++) {
                var go = new GameObject("img" + num, typeof(Image));
                var img = go.GetComponent<Image>();
                img.sprite = num <= Rating ? RatingImage : NormalImage;
                img.raycastTarget = false;
                img.transform.SetParent(group.transform);

                var rt = go.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(Width, Height); // 设置大小
                rt.transform.localScale = Vector3.one; // 设置缩放为 1
                rt.transform.localPosition = Vector3.zero; // 设置 Pos 为 0
            }
        }
        
        /// <summary>
        /// 设置评分。
        /// </summary>
        /// <param name="rating">评分数。</param>
        public void SetRating(int rating) {
            Rating = rating;
            Invalidate();
        }

        /// <summary>
        /// 添加评分。（如果可以添加的话）
        /// </summary>
        public void AddRating() {
            var rating = Rating + 1;
            if (rating > NumStars) 
                return;

            SetRating(rating);
        }
        
        /// <summary>
        /// 减少评分。
        /// </summary>
        public void ReduceRating() {
            var rating = Rating - 1;
            if (rating < 0) 
                return;

            SetRating(rating);
        }

        /// <summary>
        /// 设置要显示的评分数量。
        /// </summary>
        /// <param name="num">总的评分数量</param>
        public void SetNumStars(int num) {
            if (NumStars == num)
                return;
            
            NumStars = num;
            Invalidate();
        }
        
    }
}