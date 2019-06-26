class PagedQuery
{
  int currentPage;
  int limitPerPage;

  PagedQuery({this.currentPage, this.limitPerPage});

  String getQuery()
  {
    return "currentPage=$currentPage&limitPerPage=$limitPerPage";
  }
}
