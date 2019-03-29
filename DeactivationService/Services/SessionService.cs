using DeactivationService.Models;
using DeactivationService.Procs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeactivationService.Services
{
	public class SessionService
	{
		private enum SERVICES		{ CHANGE_CUSTOMER=21, CUSTOMER_MANAGER=17 };
		public enum SESSION_STATE	{ INVALID=0, VALID, MANAGER };

		private SessionCheckProc	m_SessionCheckProc;
		private IMemoryCache		m_Cache;

		public SessionService(IConfiguration configuration, IMemoryCache memoryCache)
		{
			var connString		= configuration.GetConnectionString("mainDatabase");
			m_SessionCheckProc	= new SessionCheckProc(connString);
			m_Cache				= memoryCache;
		}

		public SESSION_STATE CheckSession(string authToken)
		{
			int sessionId = 0;
			int.TryParse(authToken, out sessionId);

			if(sessionId == 0)
				return SESSION_STATE.INVALID;

			SessionData data;
			data = CheckCache(sessionId);

			if(data == null)
				data = CheckDatabase(sessionId);

			if(data == null)
				return SESSION_STATE.INVALID;

			return SESSION_STATE.VALID;
		}

		/// <summary>
		/// Check the session
		/// Is the sesson for the correct company
		/// Does the user have change customer if not Change Customer = 21
		/// does the customer have the CM service Customer Manager = 17
		/// </summary>
		/// <param name="session"></param>
		/// <param name="cid"></param>
		/// <returns></returns>
		public SESSION_STATE CheckSession(string authToken, int cid)
		{
			int sessionId = 0;
			int.TryParse(authToken, out sessionId);

			if(sessionId == 0)
				return SESSION_STATE.INVALID;

			SessionData data;
			data = CheckCache(sessionId);

			if(data == null)
				data = CheckDatabase(sessionId);

			if(data == null)
				return SESSION_STATE.INVALID;

			if(data.cid != cid)
			{
				if(!data.HasService((int)SERVICES.CHANGE_CUSTOMER))
					return SESSION_STATE.INVALID;
			}

			if(data.HasService((int)SERVICES.CUSTOMER_MANAGER))
				return SESSION_STATE.MANAGER;

			return SESSION_STATE.VALID;
		}

		public int GetUserId(string authToken)
		{
			int sessionId = 0;
			int.TryParse(authToken, out sessionId);

			if(sessionId == 0)
				return -1;

			SessionData sd = CheckCache(sessionId);
			if(sd == null)
				sd = CheckDatabase(sessionId);

			if(sd == null)
				return -1;

			return sd.uid;
		}

		private SessionData CheckCache(int sessionId)
		{
			SessionData data = null;
			if(m_Cache.TryGetValue(sessionId, out data))
				return data;

			return null;
		}

		private SessionData CheckDatabase(int sessionId) 
		{
			SessionData sd = m_SessionCheckProc.Execute(sessionId);
			m_Cache.Set(sessionId, sd);
			return sd;
		}

		private int DecodeSessionId(string session)
		{
			byte[] data = Convert.FromBase64String(session);
			string decodedString = Encoding.UTF8.GetString(data);
			int sessionId = 0;
	
			if(int.TryParse(decodedString, out sessionId))
			{
				return sessionId;
			}

			return -1;
		}
	}
}
