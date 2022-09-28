/**
 * @fileoverview gRPC-Web generated client stub for 
 * @enhanceable
 * @public
 */

// GENERATED CODE -- DO NOT EDIT!


/* eslint-disable */
// @ts-nocheck



const grpc = {};
grpc.web = require('grpc-web');


var google_protobuf_timestamp_pb = require('google-protobuf/google/protobuf/timestamp_pb.js')
const proto = require('./category_pb.js');

/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?grpc.web.ClientOptions} options
 * @constructor
 * @struct
 * @final
 */
proto.CatagoryServiceClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options.format = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @param {string} hostname
 * @param {?Object} credentials
 * @param {?grpc.web.ClientOptions} options
 * @constructor
 * @struct
 * @final
 */
proto.CatagoryServicePromiseClient =
    function(hostname, credentials, options) {
  if (!options) options = {};
  options.format = 'text';

  /**
   * @private @const {!grpc.web.GrpcWebClientBase} The client
   */
  this.client_ = new grpc.web.GrpcWebClientBase(options);

  /**
   * @private @const {string} The hostname
   */
  this.hostname_ = hostname;

};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.CategoryProto,
 *   !proto.CategoryResponse>}
 */
const methodDescriptor_CatagoryService_Insert = new grpc.web.MethodDescriptor(
  '/CatagoryService/Insert',
  grpc.web.MethodType.UNARY,
  proto.CategoryProto,
  proto.CategoryResponse,
  /**
   * @param {!proto.CategoryProto} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.CategoryResponse.deserializeBinary
);


/**
 * @param {!proto.CategoryProto} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.CategoryResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.CategoryResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.insert =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/Insert',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Insert,
      callback);
};


/**
 * @param {!proto.CategoryProto} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.CategoryResponse>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.insert =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/Insert',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Insert);
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.Empty,
 *   !proto.Categories>}
 */
const methodDescriptor_CatagoryService_GetAll = new grpc.web.MethodDescriptor(
  '/CatagoryService/GetAll',
  grpc.web.MethodType.UNARY,
  proto.Empty,
  proto.Categories,
  /**
   * @param {!proto.Empty} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.Categories.deserializeBinary
);


/**
 * @param {!proto.Empty} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.Categories)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.Categories>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.getAll =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/GetAll',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetAll,
      callback);
};


/**
 * @param {!proto.Empty} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.Categories>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.getAll =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/GetAll',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetAll);
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.PagingCategoryRequest,
 *   !proto.PagingCategoryResponse>}
 */
const methodDescriptor_CatagoryService_GetPaging = new grpc.web.MethodDescriptor(
  '/CatagoryService/GetPaging',
  grpc.web.MethodType.UNARY,
  proto.PagingCategoryRequest,
  proto.PagingCategoryResponse,
  /**
   * @param {!proto.PagingCategoryRequest} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.PagingCategoryResponse.deserializeBinary
);


/**
 * @param {!proto.PagingCategoryRequest} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.PagingCategoryResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.PagingCategoryResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.getPaging =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/GetPaging',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetPaging,
      callback);
};


/**
 * @param {!proto.PagingCategoryRequest} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.PagingCategoryResponse>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.getPaging =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/GetPaging',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetPaging);
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.CategoryRowIdFilter,
 *   !proto.CategoryProto>}
 */
const methodDescriptor_CatagoryService_GetById = new grpc.web.MethodDescriptor(
  '/CatagoryService/GetById',
  grpc.web.MethodType.UNARY,
  proto.CategoryRowIdFilter,
  proto.CategoryProto,
  /**
   * @param {!proto.CategoryRowIdFilter} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.CategoryProto.deserializeBinary
);


/**
 * @param {!proto.CategoryRowIdFilter} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.CategoryProto)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.CategoryProto>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.getById =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/GetById',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetById,
      callback);
};


/**
 * @param {!proto.CategoryRowIdFilter} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.CategoryProto>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.getById =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/GetById',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_GetById);
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.CategoryProto,
 *   !proto.CategoryResponse>}
 */
const methodDescriptor_CatagoryService_Put = new grpc.web.MethodDescriptor(
  '/CatagoryService/Put',
  grpc.web.MethodType.UNARY,
  proto.CategoryProto,
  proto.CategoryResponse,
  /**
   * @param {!proto.CategoryProto} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.CategoryResponse.deserializeBinary
);


/**
 * @param {!proto.CategoryProto} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.CategoryResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.CategoryResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.put =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/Put',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Put,
      callback);
};


/**
 * @param {!proto.CategoryProto} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.CategoryResponse>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.put =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/Put',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Put);
};


/**
 * @const
 * @type {!grpc.web.MethodDescriptor<
 *   !proto.CategoryRowIdFilter,
 *   !proto.CategoryResponse>}
 */
const methodDescriptor_CatagoryService_Delete = new grpc.web.MethodDescriptor(
  '/CatagoryService/Delete',
  grpc.web.MethodType.UNARY,
  proto.CategoryRowIdFilter,
  proto.CategoryResponse,
  /**
   * @param {!proto.CategoryRowIdFilter} request
   * @return {!Uint8Array}
   */
  function(request) {
    return request.serializeBinary();
  },
  proto.CategoryResponse.deserializeBinary
);


/**
 * @param {!proto.CategoryRowIdFilter} request The
 *     request proto
 * @param {?Object<string, string>} metadata User defined
 *     call metadata
 * @param {function(?grpc.web.RpcError, ?proto.CategoryResponse)}
 *     callback The callback function(error, response)
 * @return {!grpc.web.ClientReadableStream<!proto.CategoryResponse>|undefined}
 *     The XHR Node Readable Stream
 */
proto.CatagoryServiceClient.prototype.delete =
    function(request, metadata, callback) {
  return this.client_.rpcCall(this.hostname_ +
      '/CatagoryService/Delete',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Delete,
      callback);
};


/**
 * @param {!proto.CategoryRowIdFilter} request The
 *     request proto
 * @param {?Object<string, string>=} metadata User defined
 *     call metadata
 * @return {!Promise<!proto.CategoryResponse>}
 *     Promise that resolves to the response
 */
proto.CatagoryServicePromiseClient.prototype.delete =
    function(request, metadata) {
  return this.client_.unaryCall(this.hostname_ +
      '/CatagoryService/Delete',
      request,
      metadata || {},
      methodDescriptor_CatagoryService_Delete);
};


module.exports = proto;

