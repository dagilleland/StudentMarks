using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentMarks.Models
{
    //     | (\_/)
    //     | ('.')
    //     | / ! \
    //     |(")_(")
    public abstract class ValueType<T> // :IEqualityComparer<T>
        where T : ValueType<T>
    {
        public static bool operator ==(ValueType<T> first, ValueType<T> second)
        {
            if ((object)first == null)
                return (object)second == null;
            return first.Equals(second);
        }
        public static bool operator !=(ValueType<T> first, ValueType<T> second)
        {
            return !(first == second);
        }
        public override bool Equals(object obj)
        {
            if (obj is T)
                return Equals((T)obj);
            return base.Equals(obj);
        }
        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return PropertyValuesEqual(other);
        }

        /// <summary>
        /// Determines whether the properties of the specified <see cref="ValueType<T>"/> are equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract bool PropertyValuesEqual(T other);

        /// <summary>
        /// Serves as a hash function for a particular <see cref="ValueType<T>"/>.
        /// </summary>
        /// <returns></returns>
        public abstract int GetHashCode();
    }

    public abstract class AbstractEvaluationComponent<T> : ValueType<T> where T : AbstractEvaluationComponent<T>
    {
        public string Title { get; private set; }
        public int Weight { get; private set; }
        public bool IsControlled { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractEvaluationComponent"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="isControlled"></param>
        public AbstractEvaluationComponent(string title, int weight, bool isControlled)
        {
            if (String.IsNullOrEmpty(title))
                throw new ArgumentException("title is null or empty.", "title");
            if (weight <= 0 || weight > 100)
                throw new ArgumentException("weight must be between 1 and 100", "weight");
            Title = title;
            Weight = weight;
            IsControlled = isControlled;
        }
    }
    public class EvaluationComponent : AbstractEvaluationComponent<EvaluationComponent>, IEquatable<EvaluationComponent>
    {
        #region Implement ValueType<T>
        public override bool PropertyValuesEqual(EvaluationComponent other)
        {
            return Equals(this.Title, other.Title) && this.Weight.Equals(other.Weight) && this.IsControlled.Equals(other.IsControlled);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                if (this.Title != null)
                    hashCode = (hashCode * 53) ^ this.Title.GetHashCode();
                hashCode = (hashCode * 53) ^ this.Weight.GetHashCode();
                hashCode = (hashCode * 53) ^ this.IsControlled.GetHashCode();
                return hashCode;
            }
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationComponent"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="isControlled"></param>
        public EvaluationComponent(string title, int weight, bool isControlled)
            :base(title, weight, isControlled)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationComponent"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        public EvaluationComponent(string title, int weight)
            : this(title, weight, false)
        {
        }
    }

    public class EvaluationSet : AbstractEvaluationComponent<EvaluationSet>, IEquatable<EvaluationSet>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationSet"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="isControlled"></param>
        public EvaluationSet(string title, int weight, bool isControlled)
            : base(title, weight, isControlled)
        {
            Parts = new List<EvaluationComponent>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EvaluationSet"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        public EvaluationSet(string title, int weight)
            : this(title, weight, false)
        {
        }
        public ICollection<EvaluationComponent> Parts { get; set; }

        public override bool PropertyValuesEqual(EvaluationSet other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public class SubComponent : AbstractEvaluationComponent<SubComponent>, IEquatable<SubComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubComponent"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="isControlled"></param>
        public SubComponent(EvaluationSet parent, string title, int weight, bool isControlled)
            : base(title, weight, isControlled)
        {
            if (parent == null)
                throw new ArgumentException("parent is null", "parent");
            ParentSet = parent;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubComponent"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        public SubComponent(EvaluationSet parent, string title, int weight)
            : this(parent, title, weight, false)
        {
        }
        public EvaluationSet ParentSet { get; private set; }

        public override bool PropertyValuesEqual(SubComponent other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

    public class SharedWeight : AbstractEvaluationComponent<SharedWeight>, IEquatable<SharedWeight>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedWeight"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="isControlled"></param>
        public SharedWeight(EvaluationSet parent, string title, int weight, bool isControlled)
            : base(title, weight, isControlled)
        {
            ParentSet = parent;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SharedWeight"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        public SharedWeight(EvaluationSet parent, string title, int weight)
            : this(parent, title, weight, false)
        {
        }
        public EvaluationSet ParentSet { get; private set; }

        public override bool PropertyValuesEqual(SharedWeight other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
