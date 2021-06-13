using System;
using System.Collections;
using System.Collections.Generic;
using PAT.Common.Classes.Expressions.ExpressionClass;
using System.Security.Cryptography;
using System.Text; 

namespace PAT.Lib {

    public class ManagementNode : ExpressionValue {
        public int mid;
        public int deposit;
        public int numActivities;
        public int numSelected = 1;
        public int numParticipated = 1;
        public int numMisses;
        public int sDeposit;
        public int round;
        public int blockReward;
       
        public ManagementNode() {
            mid = -1;
            deposit = 1;
            numActivities = 1;
            numMisses = 1;
            numSelected = 1;
        }

        public ManagementNode(int _mid) {
            mid = _mid;
        }
        
        public ManagementNode(int _deposit, int _numActivities) {
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ManagementNode(int _mid, int _deposit, int _numActivities) {
            mid = _mid;
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ManagementNode(int _mid, int _deposit, int _numActivities, int _numMisses) {
            mid = _mid;
            deposit = _deposit;
            numActivities = _numActivities;
            numMisses = _numMisses;
        }
        
        public int computeHash(int a1, int a2) {        	
        	SHA256 sha256Hash = SHA256.Create();
        	int bytes = BitConverter.ToInt32(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("a1" + a2)), 0);
        	return bytes;
    	}
        
        public void SetMId(int _mid) {
            mid = _mid;
        }

        public int GetMId() {
            return mid;
        }
        
        public void SetRound(int _round) {
            round = _round;
        }

        public int GetRound() {
            return round;
        }
        
        public void SetDeposit(int _deposit) {
            deposit = _deposit;
        }

        public int GetDeposit() {
            return deposit;
        }
        
        public int GetEscrow() {
            return sDeposit;
        }
        
        public void AddDeposit(int _deposit) {
            deposit = this.deposit + _deposit;
        }
        
        public int SubmitDeposit(int _deposit) {
            sDeposit = _deposit;
            return sDeposit;
        }
        
       public void SetNumActivities(int _numActivities) {
            numActivities = _numActivities;
        }
        
        public void UpdateNumActivities(int _numActivities) {
            numActivities = this.numActivities + _numActivities;
        }
        
        public void UpdateNumSelected(int _numSelected) {
            numSelected = this.numSelected + _numSelected;
        }
        
        public void UpdateNumParticipated(int _numParticipated){
        	numParticipated = this.numParticipated + _numParticipated;
        }
        
        public int GetNumSelected() {
            return numSelected;
        }
        
        public int GetNumParticipated() {
            return numParticipated;
        }
        
        public int GetNumActivities() {
            return numActivities;
        }
        
        public int GetNumMisses() {
            return numMisses;
        }
        
        public void SetNumMisses(int _numMisses) {
            numMisses = _numMisses;
        }
        
        public void UpdateNumMisses(int _numMisses) {
            numMisses = this.numMisses + _numMisses;
        }
        
        public int GetReward(int _blockReward){
        	return blockReward = _blockReward;
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
        
        public override ExpressionValue GetClone() {
            return new ManagementNode(mid);
        }
        
        public override string ExpressionID {
           get {
               return "M" + mid;
           }
        }
	}
	
	public class ManagementNodes : ExpressionValue {
        public List<ManagementNode> mnset;
        
        public ManagementNodes() {
            this.mnset = new List<ManagementNode>();
        }

        public ManagementNodes(List<ManagementNode> mnset) {
            this.mnset = mnset;            
        }
        
        public ManagementNode GetNode(int index) {
            return this.mnset[index];
        }
        
        public void Nullify(ManagementNode mnode){
        	mnode = null; 
        }
        
        public int GetTotalDeposit() {
            int totalDeposit = 1;
            foreach (ManagementNode n in mnset) {
            	totalDeposit += n.GetDeposit();
            }
            return totalDeposit;
         }
         
         public int GetTotalNumActivities() {
            int totalNumActivities = 1;
            foreach (ManagementNode n in mnset) {
            	totalNumActivities += n.GetNumActivities();
            }
            return totalNumActivities;
         }
         
         public int GetTotalNumMisses(){
         	int totalNumMisses = 1;
         	foreach (ManagementNode n in mnset) {
            	totalNumMisses += n.GetNumMisses();
            }
            return totalNumMisses;
         	
         }
        
        public void Add(ManagementNode mnode) {
            bool contains = false;
            foreach (ManagementNode n in mnset) {
                if(mnode.GetMId() == n.GetMId()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                mnset.Add(mnode);
            }
        }
 
        public void Remove(ManagementNode mnode, int index) {
            if (index >= 0){
            	mnset.RemoveAt(index);
            }
        }
        
        public int GetLength() {
            return this.mnset.Count;
        }
        
        public override ExpressionValue GetClone() {
            return new ManagementNodes(new List<ManagementNode>(this.mnset));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (ManagementNode t in mnset) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
     public class ValidatorNode : ExpressionValue {
        public int vid;
        public int deposit;
        public int sDeposit;
        public int numActivities;
        public int misses;
        public int numSelected = 1;
        public ProviderNode pNode;
        public ConsumerNode cNode;
        public ManagementNode mNode;
        public List<Transaction> selectedTransactions;
        public List<Transaction> transactionList;
       
        public ValidatorNode() {
            vid = -1;
            deposit = 0;
            numActivities = 0;
            numSelected = 1;
        }

        public ValidatorNode(int _vid) {
            vid = _vid;
        }
        
        public ValidatorNode(int _deposit, int _numActivities) {
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ValidatorNode(int _vid, int _deposit, int _numActivities) {
            vid = _vid;
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ValidatorNode(int _vid, int _deposit, int _numActivities, ProviderNode _pNode) {
            vid = _vid;
            deposit = _deposit;
            numActivities = _numActivities;
            pNode = _pNode;
        }
        
        public ValidatorNode(int _vid, int _deposit, int _numActivities, ConsumerNode _cNode) {
            vid = _vid;
            deposit = _deposit;
            numActivities = _numActivities;
            cNode = _cNode;
        }
        
        public ValidatorNode(int _vid, int _deposit, int _numActivities, int _misses, ManagementNode _mNode) {
            vid = _vid;
            deposit = _deposit;
            numActivities = _numActivities;
            misses = _misses;
            mNode = _mNode;
        }
        
        public void AddDeposit(int _deposit) {
            deposit = this.deposit + _deposit;
        }
        
        public void SetVId(int _vid) {
            vid = _vid;
        }

        public int GetVId() {
            return vid;
        }
        
        public int SubmitDeposit(int _deposit) {
            sDeposit = _deposit;
            return sDeposit;
        }
        
        public int GetEscrow(){
        	return sDeposit;
        }
        
        public void SetDeposit(int _deposit) {
            deposit = _deposit;
        }

        public int GetDeposit() {
            return deposit;
        }
        
        public void SetNumActivities(int _numActivities) {
            numActivities = _numActivities;
        }
        
        public void UpdateNumActivities(int _numActivities) {
            numActivities = this.numActivities + _numActivities;
        }
        
        public void UpdateNumSelected(int _numSelected) {
            numSelected = this.numSelected + _numSelected;
        }
        
        public int GetNumSelected() {
            return numSelected;
        }


        public int GetNumActivities() {
            return numActivities;
        }
        
        public List<Transaction> getTTransactions(int t){
        	for (int i = 1; i < t; i++) {
        		selectedTransactions[i] = transactionList[i];
        	}
        	return selectedTransactions;
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new ValidatorNode(vid);
        }
        
        public override string ExpressionID {
           get {
               return "V" + vid;
           }
        }
	}
     
	public class ValidatorNodes : ExpressionValue {
        public List<ValidatorNode> vnset;
        public List<ValidatorNode> tvset;
        
        public ValidatorNodes() {
            this.vnset = new List<ValidatorNode>();
        }

        public ValidatorNodes(List<ValidatorNode> vnset) {
            this.vnset = vnset;            
        }
        
        public ValidatorNode GetNode(int index) {
            return this.vnset[index];
        }
        
        public int GetLength() {
            return this.vnset.Count;
        }
        
        public void SetNode(int index, ValidatorNode vnode) {
            this.vnset[index] = vnode;
        }
        
        public void Nullify(ValidatorNodes _tvset){
        	_tvset = null; 
        }
        
        public int GetTotalDeposit() {
            int totalDeposit = 1;
            foreach (ValidatorNode n in vnset) {
            	totalDeposit += n.GetDeposit();
            }
            return totalDeposit;
         }
         
         public int GetTotalNumActivities() {
            int totalNumActivities = 1;
            foreach (ValidatorNode n in vnset) {
            	totalNumActivities += n.GetNumActivities();
            }
            return totalNumActivities;
         }
        
        public void AddNode(ValidatorNode vnode) {
            bool contains = false;
            foreach (ValidatorNode n in vnset) {
                if(vnode.GetVId() == n.GetVId()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                vnset.Add(vnode);
            }
        }
        
        public void Empty(){
        	this.vnset.Clear();
        }
       
        public override ExpressionValue GetClone() {
            return new ValidatorNodes(new List<ValidatorNode>(this.vnset));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (ValidatorNode t in vnset) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
     public class ProviderNode : ExpressionValue {
        public int pid;
        public int deposit;
        public int numActivities;
       
        public ProviderNode() {
            pid = -1;
            deposit = 0;
            numActivities = 0;
        }

        public ProviderNode(int _pid) {
            pid = _pid;
        }
        
        public ProviderNode(int _deposit, int _numActivities) {
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ProviderNode(int _pid, int _deposit, int _numActivities) {
            pid = _pid;
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public void SetPId(int _pid) {
            pid = _pid;
        }

        public int GetPId() {
            return pid;
        }
        
        public void SubmitDeposit(int _deposit) {
            deposit = _deposit;
        }
        
        public void AddDeposit(int _deposit) {
            deposit = this.deposit + _deposit;
        }

        public int GetDeposit() {
            return deposit;
        }
        
        public void SetNumActivities(int _numActivities) {
            numActivities = _numActivities;
        }
        
        public void UpdateNumActivities(int _numActivities) {
            numActivities = this.numActivities + _numActivities;
        }
        
        public int GetNumActivities() {
            return numActivities;
        }
      
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new ProviderNode(pid);
        }
        
        public override string ExpressionID {
           get {
               return "P" + pid;
           }
        }
	}
	
	public class ProviderNodes : ExpressionValue {
        public List<ProviderNode> pnset;
        public List<ProviderNode> tvset;
        
        public ProviderNodes() {
            this.pnset = new List<ProviderNode>();
        }

        public ProviderNodes(List<ProviderNode> pnset) {
            this.pnset = pnset;            
        }
        
        public ProviderNode GetNode(int index) {
            return this.pnset[index];
        }
        
        public int GetLength() {
            return this.pnset.Count;
        }
        
        public void SetNode(int index, ProviderNode pnode) {
            this.pnset[index] = pnode;
        }
        
        public void Add(ProviderNode pnode) {
            bool contains = false;
            foreach (ProviderNode n in pnset) {
                if(pnode.GetPId() == n.GetPId()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                pnset.Add(pnode);
            }
        }
       
        public override ExpressionValue GetClone() {
            return new ProviderNodes(new List<ProviderNode>(this.pnset));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (ProviderNode t in pnset) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
	
	public class ConsumerNode : ExpressionValue {
        public int cid;
        public int deposit;
        public int numActivities;
       
        public ConsumerNode() {
            cid = -1;
            deposit = 0;
            numActivities = 0;
        }

        public ConsumerNode(int _cid) {
            cid = _cid;
        }
        
        public ConsumerNode(int _deposit, int _numActivities) {
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public ConsumerNode(int _cid, int _deposit, int _numActivities) {
            cid = _cid;
            deposit = _deposit;
            numActivities = _numActivities;
        }
        
        public void SetCId(int _cid) {
            cid = _cid;
        }

        public int GetCId() {
            return cid;
        }
        
        public int SubmitDeposit(int _deposit) {
            deposit = _deposit;
            return deposit;
        }
        
        public void AddDeposit(int _deposit) {
            deposit = this.deposit + _deposit;
        }

        public int GetDeposit() {
            return deposit;
        }
        
        public void SetNumActivities(int _numActivities) {
            numActivities = _numActivities;
        }
        
        public void UpdateNumActivities(int _numActivities) {
            numActivities = this.numActivities + _numActivities;
        }
        
        public int GetNumActivities() {
            return numActivities;
        }
       
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new ConsumerNode(cid);
        }

        public override string ExpressionID {
           get {
               return "C" + cid;
           }
        }
	}
	
	public class ConsumerNodes : ExpressionValue {
        public List<ConsumerNode> cnset;
        
        public ConsumerNodes() {
            this.cnset = new List<ConsumerNode>();
        }

        public ConsumerNodes(List<ConsumerNode> cnset) {
            this.cnset = cnset;            
        }
        
        public ConsumerNode GetNode(int index) {
            return this.cnset[index];
        }
        
        public int GetLength() {
            return this.cnset.Count;
        }
        
        public void SetNode(int index, ConsumerNode cnode) {
            this.cnset[index] = cnode;
        }
        
        public void Add(ConsumerNode cnode) {
            bool contains = false;
            foreach (ConsumerNode n in cnset) {
                if(cnode.GetCId() == n.GetCId()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                cnset.Add(cnode);
            }
        }
 
        public override ExpressionValue GetClone() {
            return new ConsumerNodes(new List<ConsumerNode>(this.cnset));
        }
 
        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (ConsumerNode t in cnset) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
    public class Transaction : ExpressionValue {
    	public string fromAdd;
    	public string toAdd;
    	public int amount;
    	public int tvote;
    	public int transactionHash;
    	public TransactionVote tVote;
    	
    	public Transaction() {
    		
    	}
    	
    	public Transaction(int _tvote) {
    		this.tvote = _tvote;
    	}
    	
    	public Transaction(string _from, string _to, int _amount) {
    		this.fromAdd = _from;
    		this.toAdd = _to;
    		this.amount = _amount;
    		this.tvote = 0;
    	}
    	
    	public int computeTHash()
    	{
        	SHA256 sha256Hash = SHA256.Create();
        	transactionHash = BitConverter.ToInt32(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("this.fromAdd" + this.toAdd + this.amount)), 0);
        	return transactionHash;
    	}
    	
    	public int getVote(){
        	return tvote;
        }
        
        public void setVote(int _tvote){
        	tvote = this.tvote + _tvote;
        }
        
        public int getVotes(){
        	return tvote = tVote.GetTVotes();
        }
        
        public int getTransactionHash(){
        return transactionHash;
        }
    	
    	public override string ToString() {
            return "[" + ExpressionID + "]";
        }
        
        public override ExpressionValue GetClone() {
            return new Transaction(fromAdd, toAdd, amount);
        }
        
        public override string ExpressionID {
           get {
               return "T" + fromAdd + toAdd + amount;
           }
        }
    }
    
    public class Transactions : ExpressionValue {
        public List<Transaction> transactionList;
        
        public Transactions() {
            this.transactionList = new List<Transaction>();
        }

        public Transactions(List<Transaction> _transactionList) {
            this.transactionList = _transactionList;            
        }
        
        
        public void Add(Transaction _transaction){
        	this.transactionList.Add(_transaction);
        }
        
        public Transaction Get(int index) {
            return this.transactionList[index];
        }
        
        public void Set(int index, Transaction _transaction) {
            this.transactionList[index] = _transaction;
        }
        
        public int GetLength() {
            return this.transactionList.Count;
        }
     
        public override ExpressionValue GetClone() {
            return new Transactions(new List<Transaction>(this.transactionList));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Transaction t in transactionList) {
                    returnString += t.ToString() + ",";
                }
                return returnString;
               }
        }
 
        public override string ToString() {
            return "[" + ExpressionID + "]";
        } 
     }
     
     public class TransactionProposal : ExpressionValue {
        public Transactions transactions;
        public TransactionSignature tsignature;

        public TransactionProposal() {
            transactions = new Transactions();
            tsignature = new TransactionSignature();
        }

        public TransactionProposal(Transactions t, TransactionSignature tsig) {
            transactions = new Transactions();
            tsignature = new TransactionSignature(tsig);
        }

        public Transactions GetTransactions() {
            return transactions;
        }
        
        public int GetSignature() {
            return tsignature.GetTSignature();
        }

        public override ExpressionValue GetClone() {
            return new TransactionProposal(transactions, tsignature);
        }

        public override string ExpressionID {
           get {
               return "TransactionProposal [B" + transactions + " by P" + tsignature.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class TransactionsProposals : ExpressionValue {
        public List<TransactionProposal> tplist;
        
        public TransactionsProposals() {
            this.tplist = new List<TransactionProposal>();
        }

        public TransactionsProposals(List<TransactionProposal> _tplist) {
            this.tplist = _tplist;            
        }
        
        public void Set(int index, TransactionProposal tproposal) {
            while(index >= tplist.Count) {
               this.tplist.Add(new TransactionProposal()); 
            }
            this.tplist[index] = tproposal;
        }

        public TransactionProposal Get(int index) {
            return this.tplist[index];
        }

        public override ExpressionValue GetClone() {
            return new TransactionsProposals(new List<TransactionProposal>(this.tplist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (TransactionProposal p in tplist) {
                    returnString += p.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    } 
     
     public class TransactionVote : ExpressionValue, IComparable {
        public int transactionHash;
        public TransactionSignature tsignature;
        public int totalVotes;
        public Transactions transactions;

        public TransactionVote() {
            transactionHash = -1;
            tsignature = new TransactionSignature();
        }
        
        public TransactionVote(Transactions _transactions, TransactionSignature sig) {
            transactions = _transactions;
            tsignature = new TransactionSignature(sig);
        }

        public int GetTransactionHash() {
            return transactionHash;
        }
        
        public int GetSignature() {
            return tsignature.GetTSignature();
        }
        
        public void updateVotes(int _totalVotes){
        	totalVotes = this.totalVotes + _totalVotes;
        }
        
        public int GetTVotes(){
        	return totalVotes; 
        }

        int IComparable.CompareTo(object tobj) {
            TransactionVote tv = (TransactionVote) tobj;
            return String.Compare(this.ToString(), tv.ToString());
        }

        public override ExpressionValue GetClone() {
            return new TransactionVote(transactions, tsignature);
        }

        public override string ExpressionID {
           get {
               return "TVote [T" + transactions + " by V" + tsignature.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class TransactionVoteSet : ExpressionValue {
        public SortedList<TransactionVote> tset;
        public Dictionary<int, int> counter;

        public TransactionVoteSet() {
            this.tset = new SortedList<TransactionVote>();
            this.counter = new Dictionary<int, int>();
        }

        public TransactionVoteSet(SortedList<TransactionVote> _tset, Dictionary<int, int> counter) {
            this.tset = _tset;
            this.counter = counter;
        }
        
        public int Size() {
            return this.tset.Count;
        }

        public void Clear() {
            tset.Clear();
            counter.Clear();
        }

        public void Add(TransactionVote tvote) {
            bool contains = false;
            foreach (TransactionVote tv in tset) {
                if(tvote.GetTransactionHash() == tv.GetTransactionHash() && tvote.GetSignature() == tv.GetSignature()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                tset.Add(tvote);
                if(counter.ContainsKey(tvote.GetTransactionHash())) {
                    counter[tvote.GetTransactionHash()]++;
                } else {
                    counter.Add(tvote.GetTransactionHash(), 1);
                }
            }
        }

        public TransactionsSignatures getSignaturesForTransaction(int transactionHash) {
            List<TransactionSignature> tsignatures = new List<TransactionSignature>();
            foreach (TransactionVote v in tset) {
                if(v.GetTransactionHash() == transactionHash) {
                    tsignatures.Add(new TransactionSignature(v.GetSignature()));
                }
            }
            return new TransactionsSignatures(new List<TransactionSignature>(tsignatures));
        }

        public override ExpressionValue GetClone() {
            return new TransactionVoteSet(new SortedList<TransactionVote>(new List<TransactionVote>(this.tset.GetInternalList())), new Dictionary<int, int>(this.counter));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (KeyValuePair<int, int> kvp in counter) {
                    returnString += string.Format("{0}:{1};", kvp.Key, kvp.Value);
                }
                foreach (TransactionVote tv in tset) {
                    returnString += tv.ToString() + ",";
                }
                return returnString;
            }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
     
    public class TransactionSignature: ExpressionValue {
        public int tSignature;

        public TransactionSignature() {
            tSignature = -1;
        }

        public TransactionSignature(int _tSignature) {
            tSignature = _tSignature;
        }
        
        public TransactionSignature(TransactionSignature _tSignature) {
            tSignature = _tSignature.GetTSignature();
        }
        
        public void SetTSignature(int _tSignature) {
            tSignature = _tSignature;
        }

        public int GetTSignature() {
            return tSignature;
        }
	
        public override string ToString() {
            return tSignature.ToString();
        }

        public override ExpressionValue GetClone() {
            return new TransactionSignature(tSignature);
        }

        public override string ExpressionID {
           get {
               return tSignature.ToString();
           }
        }
    }
    
    public class TransactionsSignatures : ExpressionValue {
        public List<TransactionSignature> tlist;
        
        public TransactionsSignatures() {
            this.tlist = new List<TransactionSignature>();
        }

        public TransactionsSignatures(List<TransactionSignature> _tlist) {
            this.tlist = _tlist;            
        }

        public List<TransactionSignature> GetList() {
            return this.tlist;
        }

        public override ExpressionValue GetClone() {
            return new TransactionsSignatures(new List<TransactionSignature>(this.tlist));
        }
 
        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (TransactionSignature s in tlist) {
                    returnString += s.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    } 
     
     public class Block : ExpressionValue {
        public int index;
        public int bHash;
        public int prevBlockHash;
        public Transactions pendingTransactions;
        public BlocksSignatures bsignatures;
        public ManagementNode leader;
       
        public Block() {
            bHash = -1;
            bsignatures = new BlocksSignatures();
        }

        public Block(int _bHash) {
            bHash = _bHash;
            bsignatures = new BlocksSignatures();
        }

        public Block(int _bHash, BlocksSignatures bsigs) {
            bHash = _bHash;
            bsignatures = new BlocksSignatures(new List<BlockSignature>(bsigs.GetList()));
        }
        
        public Block(int _index, ManagementNode _leader, int _prevBlockHash, Transactions _pendingTransactions) {
            this.index = _index;
            this.leader = _leader;
            this.prevBlockHash = _prevBlockHash;
            this.bHash = this.computeHash();
            this.pendingTransactions = _pendingTransactions;
            bsignatures = new BlocksSignatures();
        }

        public void SetSignatureList(BlocksSignatures bsigs) {
            bsignatures = new BlocksSignatures(new List<BlockSignature>(bsigs.GetList()));
        }

        public BlocksSignatures GetSignatureList() {
            return bsignatures;
        }
        
        public void SetHash(int _bHash) {
            bHash = _bHash;
        }

        public int GetBlockHash() {
            return bHash;
        }
        
        public int GetPrevHash() {
            return prevBlockHash;
        }
        
        public Transactions GetTransactions() {
            return pendingTransactions;
        }
        
        public int computeHash() {        	
        	SHA256 sha256Hash = SHA256.Create();
        	int bytes = BitConverter.ToInt32(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes("this.index" + this.leader + this.prevBlockHash + this.pendingTransactions)), 0);
        	return bytes;
    	}
    	
    	public int GetIndex(){
    		return index;
    	}
    	
    	public ManagementNode GetLeader(){
    		return leader;
    	}

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }

        public override ExpressionValue GetClone() {
            return new Block(bHash, new BlocksSignatures(new List<BlockSignature>(bsignatures.GetList())));
        }

        public override string ExpressionID {
           get {
               return "B" + bHash;
           }
        }
    }
    
    public class Blocks : ExpressionValue {
        public List<Block> blist;
        
        public Blocks() {
            this.blist = new List<Block>();
        }

        public Blocks(List<Block> _blist) {
            this.blist = _blist;            
        }
        
        public void Set(int index, Block _block) {
            while(index >= blist.Count) {
               this.blist.Add(new Block()); 
            }
            this.blist[index] = _block;
        }

        public Block Get(int index) {
            if(index >= blist.Count) {
                return new Block();
            }
            return this.blist[index];
        }

        public void Clear() {
            this.blist.Clear();
        }

        public override ExpressionValue GetClone() {
            return new Blocks(new List<Block>(this.blist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Block b in blist) {
                    returnString += b.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
     
     public class Blockchain : ExpressionValue {
        public List<Block> blockchain;
        public Transactions pendingTransactions;
        public int mReward;
        public int escrowAmount;
        
        public Blockchain() {
            this.blockchain = new List<Block>();
            this.pendingTransactions = null;   
        }

        public Blockchain(List<Block> _blockchain) {
            this.blockchain = _blockchain;            
        }
        
        public Block CreateGenesisBlock(){
    		return new Block();
    	}
        
        public Block GetPeekBlock() {
            if(this.blockchain.Count == 0) {
                return new Block();
            }
            return this.blockchain[this.blockchain.Count-1];
        }
        
        public ManagementNode GetBlockLeader(Block _block){
        	return _block.GetLeader();
        }
        
        public void Nullify(Block _block){
        	_block = null;
        }
        
        public int GetHeight() {
            return this.blockchain.Count;
        }
        
        public void AddBlock(Block _block) {
            this.blockchain.Add(_block);
        }
        
        public void SetEscrowAmount(int amount) {
            escrowAmount = this.escrowAmount + amount;
        }
        
        public void RemoveEscrowAmount(int amount) {
            escrowAmount = this.escrowAmount - amount;
        }
        
        public bool ContainsBlock(Block _block) {
            foreach (Block b1 in blockchain) {
                if(b1.GetBlockHash() == _block.GetBlockHash()) {
                    return true;
                }
            }
            return false;
        }

        public bool IsEmpty() {
            return this.blockchain.Count == 0;
        }

        public bool ContainsDuplicateBlocks() {
            List<Block> tmpList = new List<Block>();
            foreach (Block b1 in blockchain) {
                if (!tmpList.Contains(b1)) {
                    tmpList.Add(b1);
                } else {
                    return true;
                }
            }
            return false;
        }

        public Block GetBlock(int index) {
            return this.blockchain[index];
        }
        
        public override ExpressionValue GetClone() {
            return new Blockchain(new System.Collections.Generic.List<Block>(this.blockchain));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (Block b in blockchain) {
                    returnString += b.GetBlockHash() + ",";
                }
                return returnString;
            }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
    public class BlockSignature: ExpressionValue {
        public int blockSignature;
        
        public BlockSignature() {
            blockSignature = -1;
        }

        public BlockSignature(int _blockSignature) {
            blockSignature = _blockSignature;
        }
        
        public BlockSignature(BlockSignature _blockSignature) {
            blockSignature = _blockSignature.GetBSignature();
        }
        
        public void SetBSignature(int _blockSignature) {
            blockSignature = _blockSignature;
        }

        public int GetBSignature() {
            return blockSignature;
        }
        
        public override string ToString() {
            return blockSignature.ToString();
        }

        public override ExpressionValue GetClone() {
            return new BlockSignature(blockSignature);
        }
 
        public override string ExpressionID {
           get {
               return blockSignature.ToString();
           }
        }
    }
    
    public class BlocksSignatures : ExpressionValue {
        public List<BlockSignature> slist;
        
        public BlocksSignatures() {
            this.slist = new List<BlockSignature>();
        }

        public BlocksSignatures(List<BlockSignature> _slist) {
            this.slist = _slist;            
        }

        public List<BlockSignature> GetList() {
            return this.slist;
        }

        public override ExpressionValue GetClone() {
            return new BlocksSignatures(new List<BlockSignature>(this.slist));
        }
 
        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (BlockSignature s in slist) {
                    returnString += s.ToString() + ",";
                }
                return returnString;
               }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
    public class BlockProposal : ExpressionValue {
        public Block block;
        public BlockSignature bsignature;
        
        public BlockProposal() {
            block = new Block();
            bsignature = new BlockSignature();
        }

        public BlockProposal(Block b, BlockSignature bsig) {
            block = new Block(b.GetBlockHash());
            bsignature = new BlockSignature(bsig);
        }

        public Block GetBlock() {
            return block;
        }
        
        public int GetSignature() {
            return bsignature.GetBSignature();
        }

        public override ExpressionValue GetClone() {
            return new BlockProposal(block, bsignature);
        }

        public override string ExpressionID {
           get {
               return "BlockProposal [BP" + block.GetBlockHash() + " by L" + bsignature.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class BlocksProposals : ExpressionValue {
        public List<BlockProposal> bplist;
        
        public BlocksProposals() {
            this.bplist = new List<BlockProposal>();
        }

        public BlocksProposals(List<BlockProposal> _bplist) {
            this.bplist = _bplist;            
        }
        
        public void SetProposal(int index, BlockProposal bproposal) {
            while(index >= bplist.Count) {
               this.bplist.Add(new BlockProposal()); 
            }
            this.bplist[index] = bproposal;
        }

        public BlockProposal GetProposal(int index) {
            return this.bplist[index];
        }

        public override ExpressionValue GetClone() {
            return new BlocksProposals(new List<BlockProposal>(this.bplist));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (BlockProposal p in bplist) {
                    returnString += p.ToString() + ",";
                }
                return returnString;
               }
        }
        
        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    } 
    
    public class BlockVote : ExpressionValue, IComparable {
        public int blockHash;
        public BlockSignature bsignature;
        public int totalVotes;

        public BlockVote() {
            blockHash = -1;
            bsignature = new BlockSignature();
        }

        public BlockVote(int bh, BlockSignature bsig) {
            blockHash = bh;
            bsignature = new BlockSignature(bsig);
        }

        public int GetBlockHash() {
            return blockHash;
        }
        
        public int GetSignature() {
            return bsignature.GetBSignature();
        }
        
        public void updateVotes(int _totalVotes){
        	totalVotes = this.totalVotes + _totalVotes;
        	//return totalVotes;
        }
        
        public int GetVotes(){
        	return totalVotes; 
        }

        int IComparable.CompareTo(object bobj) {
            BlockVote bv = (BlockVote) bobj;
            return String.Compare(this.ToString(), bv.ToString());
        }

        public override ExpressionValue GetClone() {
            return new BlockVote(blockHash, bsignature);
        }

        public override string ExpressionID {
           get {
               return "Vote [BV" + blockHash + " by L" + bsignature.ToString() + "]";
           }
        }

        public override string ToString() {
            return ExpressionID;
        }
    }
    
    public class BlockVoteSet : ExpressionValue {
        public SortedList<BlockVote> bset;
        public Dictionary<int, int> bcounter;

        public BlockVoteSet() {
            this.bset = new SortedList<BlockVote>();
            this.bcounter = new Dictionary<int, int>();
        }

        public BlockVoteSet(SortedList<BlockVote> _bset, Dictionary<int, int> _bcounter) {
            this.bset = _bset;
            this.bcounter = _bcounter;
        }
        
        public int Size() {
            return this.bset.Count;
        }

        public void Clear() {
            bset.Clear();
            bcounter.Clear();
        }

        public void Add(BlockVote bvote) {
            bool contains = false;
            foreach (BlockVote bv in bset) {
                if(bvote.GetBlockHash() == bv.GetBlockHash() && bvote.GetSignature() == bv.GetSignature()) {
                    contains = true;
                    break;
                }
            }
            if(!contains) {
                bset.Add(bvote);
                if(bcounter.ContainsKey(bvote.GetBlockHash())) {
                    bcounter[bvote.GetBlockHash()]++;
                } else {
                    bcounter.Add(bvote.GetBlockHash(), 1);
                }
            }
        }

        public BlocksSignatures getSignaturesForBlock(int blockHash) {
            List<BlockSignature> bsignatures = new List<BlockSignature>();
            foreach (BlockVote bv in bset) {
                if(bv.GetBlockHash() == blockHash) {
                    bsignatures.Add(new BlockSignature(bv.GetSignature()));
                }
            }
            return new BlocksSignatures(new List<BlockSignature>(bsignatures));
        }

        public Block GetBlockWithMajorityVotes(int bmin) {
            foreach(KeyValuePair<int, int> bentry in bcounter) {
                if(bentry.Value >= bmin) {
                    return new Block(bentry.Key);
                }
            }
            return new Block();
        }

        public override ExpressionValue GetClone() {
            return new BlockVoteSet(new SortedList<BlockVote>(new List<BlockVote>(this.bset.GetInternalList())), new Dictionary<int, int>(this.bcounter));
        }

        public override string ExpressionID {
            get {
                string returnString = "";
                foreach (KeyValuePair<int, int> bkvp in bcounter) {
                    returnString += string.Format("{0}:{1};", bkvp.Key, bkvp.Value);
                }
                foreach (BlockVote bv in bset) {
                    returnString += bv.ToString() + ",";
                }
                return returnString;
            }
        }

        public override string ToString() {
            return "[" + ExpressionID + "]";
        }
    }
    
 public class SortedList<T> : ICollection<T> {
        private List<T> m_innerList;
        private Comparer<T> m_comparer;
    
        public SortedList() : this(new List<T>()) {}
        
        public SortedList(List<T> m_innerList) {
            this.m_innerList = m_innerList;
            this.m_comparer = Comparer<T>.Default;
        }
        
        public List<T> GetInternalList() {
            return  m_innerList;
        }
    
        public void Add(T item) {
            int insertIndex = FindIndexForSortedInsert(m_innerList, m_comparer, item);
            m_innerList.Insert(insertIndex, item);
        }
    
        public bool Contains(T item) {
            return IndexOf(item) != -1;
        }
    
        public int IndexOf(T item) {
            int insertIndex = FindIndexForSortedInsert(m_innerList, m_comparer, item);
            if (insertIndex == m_innerList.Count) {
                return -1;
            }
            if (m_comparer.Compare(item, m_innerList[insertIndex]) == 0) {
                int index = insertIndex;
                while (index > 0 && m_comparer.Compare(item, m_innerList[index - 1]) == 0) {
                    index--;
                }
                return index;
            }
            return -1;
        }
    
        public bool Remove(T item) {
            int index = IndexOf(item);
            if (index >= 0) {
                m_innerList.RemoveAt(index);
                return true;
            }
            return false;
        }
    
        public void RemoveAt(int index) {
            m_innerList.RemoveAt(index);
        }
    
        public void CopyTo(T[] array) {
            m_innerList.CopyTo(array);
        }
    
        public void CopyTo(T[] array, int arrayIndex) {
            m_innerList.CopyTo(array, arrayIndex);
        }
    
        public void Clear() {
            m_innerList.Clear();
        }
    
        public T this[int index] {
            get {
                return m_innerList[index];
            }
        }
    
        public IEnumerator<T> GetEnumerator() {
            return m_innerList.GetEnumerator();
        }
    
        IEnumerator IEnumerable.GetEnumerator() {
            return m_innerList.GetEnumerator();
        }
    
        public int Count {
            get {
                return m_innerList.Count;
            }
        }
    
        public bool IsReadOnly {
            get {
                return false;
            }
        }
    
        public static int FindIndexForSortedInsert(List<T> list, Comparer<T> comparer, T item) {
            if (list.Count == 0) {
                return 0;
            }
    
            int lowerIndex = 0;
            int upperIndex = list.Count - 1;
            int comparisonResult;
            while (lowerIndex < upperIndex) {
                int middleIndex = (lowerIndex + upperIndex) / 2;
                T middle = list[middleIndex];
                comparisonResult = comparer.Compare(middle, item);
                if (comparisonResult == 0) {
                    return middleIndex;
                } else if (comparisonResult > 0) {  
                    upperIndex = middleIndex - 1;
                } else {    
                    lowerIndex = middleIndex + 1;
                }
            }
    
            comparisonResult = comparer.Compare(list[lowerIndex], item);
            if (comparisonResult < 0) {
                return lowerIndex + 1;
            } else {
                return lowerIndex;
            }
        }
    }
}    