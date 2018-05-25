// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;

namespace _4screen.CSB.DataAccess.Business
{
    public class Rating
    {
        private RatingType enuRatingType = RatingType.Standard;
        private bool blnSingleVote = true;
        private int intMinPoint = 1;
        private int intMaxPoint = 1;
        private int intMinRating = 1;
        private int intMaxRating = 1;
        private List<Business.RatingObject> list;


        public void SetAsStandard(int minPoint, int maxPoint, Guid dataObjectID)
        {
            enuRatingType = RatingType.Standard;
            intMinPoint = minPoint;
            intMaxPoint = maxPoint;
            intMinRating = 1;
            intMaxRating = 1;

            list = new List<RatingObject>(1);
            list.Add(new Business.RatingObject(this, dataObjectID));
        }

        public void SetAsOneVersusOne(int minPoint, Guid dataObjectID1, Guid dataObjectID2)
        {
            enuRatingType = RatingType.OneVersusOne;
            intMinPoint = minPoint;
            intMaxPoint = minPoint;
            intMinRating = 1;
            intMaxRating = 2;
            list = new List<RatingObject>(2);
            list.Add(new Business.RatingObject(this, dataObjectID1));
            list.Add(new Business.RatingObject(this, dataObjectID2));
        }

        public void SetAsBestOfMany(int minPoint, int minRating, int maxRating, Guid[] dataObjectID)
        {
            enuRatingType = RatingType.OneVersusOne;
            intMinPoint = minPoint;
            intMaxPoint = minPoint;
            intMinRating = 1;
            intMaxRating = 2;

            list = new List<RatingObject>(dataObjectID.Length);
            for (int i = 0; i < dataObjectID.Length; i++)
            {
                list.Add(new Business.RatingObject(this, dataObjectID[i]));
            }
        }

        public List<Business.RatingObject> RatingObjects
        {
            get { return list; }
        }

        internal void FlipFlop(Guid objectID)
        {
            foreach (Business.RatingObject item in list)
            {
                if (item.ID != objectID)
                {
                    item.VoteThis = 0;
                }
            }
        }

        internal int CountVoting()
        {
            int intCount = 0;
            foreach (Business.RatingObject item in list)
            {
                if (item.VoteThis > 0)
                {
                    intCount++;
                }
            }
            return intCount;
        }

        public RatingType RatingType
        {
            get { return enuRatingType; }
        }

        public bool SingleVote
        {
            get { return blnSingleVote; }
            set { blnSingleVote = value; }
        }

        public int MinPoint
        {
            get { return intMinPoint; }
        }

        public int MaxPoint
        {
            get { return intMaxPoint; }
        }

        public int MinRating
        {
            get { return intMinRating; }
        }

        public int MaxRating
        {
            get { return intMaxRating; }
        }
    }

    public class RatingObject
    {
        private Guid _ObjectID ;
        //private Business.DataObject item = null;
        private Business.Rating parent;
        private int intVoteThis = 0;

        internal RatingObject(Business.Rating parent, Guid dataObjectID)
        {
            this.parent = parent;
            //this.item = dataObject;
            _ObjectID = dataObjectID;
            if (parent.RatingType == RatingType.Standard)
                intVoteThis = parent.MinPoint;
        }

        //internal RatingObject(Business.Rating parent, Business.DataObject dataObject)
        //{
        //   this.parent = parent; 
        //   //this.item = dataObject;
        //   strObjectID = dataObject.ObjectID;  
        //   if (parent.RatingType == RatingType.Standard)
        //      intVoteThis = parent.MinPoint;  
        //}

        public Guid ID
        {
            //get { return item.ObjectID; }
            get { return _ObjectID; }
        }

        public int VoteThis
        {
            get { return intVoteThis; }
            set
            {
                if (value > 0)
                {
                    if (parent.RatingType == RatingType.Standard)
                    {
                        intVoteThis = value;
                    }
                    else if (parent.RatingType == RatingType.OneVersusOne)
                    {
                        parent.FlipFlop(ID);
                        intVoteThis = 1;
                    }
                    else
                    {
                        if (parent.CountVoting() < parent.MinRating)
                        {
                            intVoteThis = 1;
                        }
                    }
                }
                else
                {
                    intVoteThis = 0;
                }
            }
        }
    }
}