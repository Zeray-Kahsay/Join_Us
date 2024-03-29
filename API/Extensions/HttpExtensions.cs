﻿using System.Text.Json;

namespace API;

public static class HttpExtensions
{
  // sends info about pagination options to the client 
  public static void AddPaginationHeader(this HttpResponse response, int currentPage,
      int itemsPerPage, int totalItems, int totalPages)
  {
    var paginationHeader = new
    {
      currentPage,
      itemsPerPage,
      totalItems,
      totalPages
    };
    response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader));
    response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
  }
}
