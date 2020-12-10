import { Pagination, PaginationItem, PaginationLink } from "reactstrap";

const PaginationWrapper = ({ next, previous, totalPages, setUrl }) => {
  const changePage = (pageNumber) => {
    const url = process.env.REACT_APP_API_URL + "api/contact/get/" + pageNumber;
    setUrl(url);
  };
  return (
    <Pagination aria-label="Page navigation example">
      <PaginationItem>
        <PaginationLink first onClick={() => changePage(1)} />
      </PaginationItem>
      {previous && (
        <PaginationItem>
          <PaginationLink previous onClick={() => setUrl(previous)} />
        </PaginationItem>
      )}
      {[...Array(totalPages).keys()].map((page) => {
        return (
          <PaginationItem key={page}>
            <PaginationLink onClick={() => changePage(page + 1)}>
              {page + 1}
            </PaginationLink>
          </PaginationItem>
        );
      })}
      {next && (
        <PaginationItem>
          <PaginationLink next onClick={() => setUrl(next)} />
        </PaginationItem>
      )}
      <PaginationItem>
        <PaginationLink last onClick={() => changePage(totalPages)} />
      </PaginationItem>
    </Pagination>
  );
};

export default PaginationWrapper;
